using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Error;
using Apps.Smartsheet.Models.Entities.Comment;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Comment;
using Apps.Smartsheet.Models.Response.Comment;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Comments")]
public class CommentActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/comments/comment-get
    [Action("Get comment", Description = "Get a specific comment")]
    public async Task<CommentResponse> GetComment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] CommentIdentifier commentIdentifier)
    {
        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/comments/{commentIdentifier.CommentId}");
        var response = await Client.ExecuteWithErrorHandling<CommentEntity>(request);

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/comments/comments-create
    [Action("Create comment", Description = "Add a comment to a discussion")]
    public async Task<CommentResponse> CreateComment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] DiscussionIdentifier discussionIdentifier,
        [ActionParameter] CreateCommentRequest createInput)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/discussions/{discussionIdentifier.DiscussionId}/comments";
        var request = new SmartsheetRequest(endpoint, Method.Post)
            .WithJsonBody(new 
            {
                text = createInput.CommentText
            });
        var response = await Client.ExecuteWithErrorHandling<Result<CommentEntity>>(request);

        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/comments/comment-edit
    [Action("Update comment", Description = "Update the text of a specific comment. Only the author can edit it")]
    public async Task<CommentResponse> UpdateComment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] CommentIdentifier commentIdentifier,
        [ActionParameter] UpdateCommentRequest updateInput)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/comments/{commentIdentifier.CommentId}";
        var request = new SmartsheetRequest(endpoint, Method.Put)
            .WithJsonBody(new 
            {
                text = updateInput.CommentText
            });
        var response = await Client.ExecuteWithErrorHandling<Result<CommentEntity>>(request);

        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/comments/comment-delete
    [Action("Delete comment", Description = "Delete a specific comment")]
    public async Task DeleteComment(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] CommentIdentifier commentIdentifier)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/comments/{commentIdentifier.CommentId}";
        var request = new SmartsheetRequest(endpoint, Method.Delete);
        var response = await Client.ExecuteWithErrorHandling<Result>(request);

        if (!response.IsSuccessfulResponse)
            throw new PluginApplicationException(ErrorMessageHelper.GenerateFailedToDeleteMessage("comment"));
    }
}