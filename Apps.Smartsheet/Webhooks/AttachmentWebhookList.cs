using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Webhook;
using Apps.Smartsheet.Models.Entities.Attachment;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Response.Attachment;
using Apps.Smartsheet.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Smartsheet.Webhooks;

[WebhookList("Attachment")]
public class AttachmentWebhookList(InvocationContext context) : SmartsheetInvocable(context)
{
    [Webhook("On attachments created", typeof(SmartsheetEventHandler),
        Description = "Triggers when an attachment is added to the sheet")]
    public async Task<WebhookResponse<SearchAttachmentsResponse>> OnAttachmentCreated(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier)
    {
        var processedEvent = WebhookHelper.ProcessEvent(request, "attachment", "created");

        if (processedEvent.EventIds == null || processedEvent.EventIds.Count == 0)
            return WebhookHelper.Preflight<SearchAttachmentsResponse>(processedEvent.Response);

        var fetchTasks = processedEvent.EventIds.Select(async targetAttachmentId =>
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
}