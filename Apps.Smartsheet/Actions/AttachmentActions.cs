using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Error;
using Apps.Smartsheet.Models.Entities.Attachment;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Attachment;
using Apps.Smartsheet.Models.Response.Attachment;
using Apps.Smartsheet.Models.Response.File;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Attachments")]
public class AttachmentActions(InvocationContext context, IFileManagementClient fileManagementClient)
    : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-listonsheet
    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/discussion-listattachments
    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-listonrow
    [Action("Search attachments", Description = "Search attachments on a sheet, discussion or row")]
    public async Task<SearchAttachmentsResponse> SearchAttachments(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalRowIdentifier rowIdentifier,
        [ActionParameter] OptionalDiscussionIdentifier discussionIdentifier)
    {
        if (!string.IsNullOrEmpty(rowIdentifier.RowId) && !string.IsNullOrEmpty(discussionIdentifier.DiscussionId))
            throw new PluginMisconfigurationException("Please specify either a row ID or a discussion ID");
        
        string endpoint;
        if (!string.IsNullOrEmpty(rowIdentifier.RowId))
            endpoint = $"sheets/{sheetIdentifier.SheetId}/rows/{rowIdentifier.RowId}/attachments";
        else if (!string.IsNullOrEmpty(discussionIdentifier.DiscussionId))
            endpoint = $"sheets/{sheetIdentifier.SheetId}/discussions/{discussionIdentifier.DiscussionId}/attachments";
        else
            endpoint = $"sheets/{sheetIdentifier.SheetId}/attachments";

        var request = new SmartsheetRequest(endpoint);
        var result = await Client.PaginateOffset<AttachmentEntity>(request)
            .Select(x => new AttachmentResponse(x))
            .ToArrayAsync();

        return new(result);
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-attachtosheet
    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-attachtocomment
    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/row-attachments-attachfile
    [Action("Upload attachment", Description = "Upload an attachment to a sheet, comment or a row")]
    public async Task<AttachmentResponse> UploadAttachment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalRowIdentifier rowIdentifier,
        [ActionParameter] OptionalCommentIdentifier commentIdentifier,
        [ActionParameter] UploadAttachmentRequest uploadInput)
    {
        if (!string.IsNullOrEmpty(rowIdentifier.RowId) && !string.IsNullOrEmpty(commentIdentifier.CommentId))
            throw new PluginMisconfigurationException("Please specify either a row ID or a comment ID");

        string endpoint;
        if (!string.IsNullOrEmpty(rowIdentifier.RowId))
            endpoint = $"sheets/{sheetIdentifier.SheetId}/rows/{rowIdentifier.RowId}/attachments";
        else if (!string.IsNullOrEmpty(commentIdentifier.CommentId))
            endpoint = $"sheets/{sheetIdentifier.SheetId}/comments/{commentIdentifier.CommentId}/attachments";
        else
            endpoint = $"sheets/{sheetIdentifier.SheetId}/attachments";

        await using var file = await fileManagementClient.DownloadAsync(uploadInput.File);
        var fileBytes = await file.GetByteData();
        
        var request = new SmartsheetRequest(endpoint, Method.Post)
            .AddHeader("Content-Disposition", $"attachment; filename=\"{uploadInput.File.Name}\"")
            .AddHeader("Content-Type", uploadInput.File.ContentType)
            .AddParameter(uploadInput.File.ContentType, fileBytes, ParameterType.RequestBody);
        
        var response = await Client.ExecuteWithErrorHandling<Result<AttachmentEntity>>(request);
        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-get
    [Action("Download attachment", Description = "Download a specific attachment")]
    public async Task<FileResponse> DownloadAttachment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] AttachmentIdentifier attachmentIdentifier)
    {
        try
        {
            var attachment = await FetchAttachment(sheetIdentifier.SheetId, attachmentIdentifier.AttachmentId);
            if (string.IsNullOrWhiteSpace(attachment.Url))
                throw new PluginMisconfigurationException("This attachment can't be downloaded");

            var client = new RestClient();
            var request = new RestRequest(attachment.Url);
            var networkStream = await client.DownloadStreamAsync(request) ??
                                throw new PluginApplicationException(
                                    "Failed to download a file: empty stream received");

            var seekableStream = new MemoryStream();
            await networkStream.CopyToAsync(seekableStream);
            seekableStream.Position = 0;

            var file = await fileManagementClient.UploadAsync(
                seekableStream,
                attachment.MimeType ?? "application/octet-stream",
                attachment.Name);

            return new(file);
        }
        catch (Exception ex)
        {
            var safeErrorPayload = new 
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                InnerException = ex.InnerException?.Message 
            };

            WebhookLogger.Log(safeErrorPayload);
            throw;
        }
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-get
    [Action("Get attachment", Description = "Get information about a specific attachment")]
    public async Task<AttachmentResponse> GetAttachment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] AttachmentIdentifier attachmentIdentifier)
    {
        var attachment = await FetchAttachment(sheetIdentifier.SheetId, attachmentIdentifier.AttachmentId);
        return new(attachment);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-delete
    [Action("Delete attachment", Description = "Delete a specific attachment")]
    public async Task DeleteAttachment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] AttachmentIdentifier attachmentIdentifier)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/attachments/{attachmentIdentifier.AttachmentId}";
        var request = new SmartsheetRequest(endpoint, Method.Delete);
        var response = await Client.ExecuteWithErrorHandling<Result>(request);

        if (!response.IsSuccessfulResponse)
            throw new PluginMisconfigurationException(ErrorMessageHelper.GenerateFailedToDeleteMessage("attachment"));
    }

    private async Task<AttachmentEntity> FetchAttachment(string sheetId, string attachmentId)
    {
        var request = new SmartsheetRequest($"sheets/{sheetId}/attachments/{attachmentId}");
        return await Client.ExecuteWithErrorHandling<AttachmentEntity>(request);
    }
}