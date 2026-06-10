using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Webhook;
using Apps.Smartsheet.Models.Entities.Attachment;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Response.Attachment;
using Apps.Smartsheet.Webhooks.Handlers;
using Apps.Smartsheet.Webhooks.Models.Response.Attachment;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Smartsheet.Webhooks;

[WebhookList("Attachment")]
public class AttachmentWebhookList(InvocationContext context) : SmartsheetInvocable(context)
{
    [Webhook("On attachments created", typeof(SmartsheetEventHandler),
        Description = "Triggers when an attachment is added to the sheet")]
    public async Task<WebhookResponse<SearchAttachmentsResponse>> OnAttachmentsCreated(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier)
    {
        var processedEvent = WebhookHelper.ProcessEvent(request, "attachment", "created");
        if (processedEvent.ShouldPreflight)
            return WebhookHelper.Preflight<SearchAttachmentsResponse>(processedEvent.Response);

        var fetchTasks = processedEvent.EventIds!.Select(async targetAttachmentId =>
        {
            var getRequest = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/attachments/{targetAttachmentId}");
            var attachmentEntity = await Client.ExecuteWithErrorHandling<AttachmentEntity>(getRequest);
            return new AttachmentResponse(attachmentEntity);
        });
        var attachments = await Task.WhenAll(fetchTasks);
        
        return new WebhookResponse<SearchAttachmentsResponse>
        {
            HttpResponseMessage = processedEvent.Response,
            Result = new(attachments)
        };
    }
    
    [Webhook("On attachments updated", typeof(SmartsheetEventHandler),
        Description = "Triggers when an attachment is updated")]
    public async Task<WebhookResponse<SearchAttachmentsResponse>> OnAttachmentsUpdated(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier,
        [WebhookParameter] OptionalAttachmentIdentifier attachmentIdentifier)
    {
        var processedEvent = WebhookHelper.ProcessEvent(request, "attachment", "updated");
        if (processedEvent.ShouldPreflight)
            return WebhookHelper.Preflight<SearchAttachmentsResponse>(processedEvent.Response);
        
        var matchingEventIds = processedEvent.EventIds!;
        if (!string.IsNullOrWhiteSpace(attachmentIdentifier.AttachmentId))
        {
            matchingEventIds = matchingEventIds
                .Where(id => id == attachmentIdentifier.AttachmentId)
                .ToList();

            if (matchingEventIds.Count == 0)
                return WebhookHelper.Preflight<SearchAttachmentsResponse>(processedEvent.Response);
        }
        
        var fetchTasks = matchingEventIds.Select(async targetAttachmentId =>
        {
            var getRequest = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/attachments/{targetAttachmentId}");
            var attachmentEntity = await Client.ExecuteWithErrorHandling<AttachmentEntity>(getRequest);
            return new AttachmentResponse(attachmentEntity);
        });
        var attachments = await Task.WhenAll(fetchTasks);
        
        return new WebhookResponse<SearchAttachmentsResponse>
        {
            HttpResponseMessage = processedEvent.Response,
            Result = new(attachments)
        };
    }
    
    [Webhook("On attachments deleted", typeof(SmartsheetEventHandler),
        Description = "Triggers when an attachment is deleted")]
    public Task<WebhookResponse<AttachmentDeletedResponse>> OnAttachmentsDeleted(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier)
    {
        var processedEvent = WebhookHelper.ProcessEvent(request, "attachment", "deleted");
        if (processedEvent.ShouldPreflight)
            return Task.FromResult(WebhookHelper.Preflight<AttachmentDeletedResponse>(processedEvent.Response));
        
        return Task.FromResult(new WebhookResponse<AttachmentDeletedResponse>
        {
            HttpResponseMessage = processedEvent.Response,
            Result = new(processedEvent.EventIds!.ToArray())
        });
    }
}