using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Webhook;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Response.Sheet;
using Apps.Smartsheet.Webhooks.Handlers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Smartsheet.Webhooks;

[WebhookList("Sheets")]
public class SheetWebhookList(InvocationContext context) : SmartsheetInvocable(context)
{
    [Webhook("On sheet updated", typeof(SmartsheetEventHandler), Description = "Triggers when a specific sheet is updated")]
    public async Task<WebhookResponse<SheetResponse>> OnSheetUpdated(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier)
    {
        var processedEvent = WebhookHelper.ProcessEvent(request, "sheet", "updated");
        if (processedEvent.ShouldPreflight)
            return WebhookHelper.Preflight<SheetResponse>(processedEvent.Response);

        var getRequest = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}");
        var sheetEntity = await Client.ExecuteWithErrorHandling<SheetEntity>(getRequest);

        return new WebhookResponse<SheetResponse>
        {
            HttpResponseMessage = processedEvent.Response,
            Result = new SheetResponse(sheetEntity)
        };
    }
}