using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Webhook;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Response.Sheet;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Smartsheet.Webhooks;

[WebhookList("Sheets")]
public class SheetWebhookList(InvocationContext context) : SmartsheetInvocable(context)
{
    [Webhook("On sheet updated", typeof(EventHandler), Description = "Triggers when a specific sheet is updated")]
    public async Task<WebhookResponse<SheetResponse>> OnSheetUpdated(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier)
    {
        var processedEvent = WebhookHelper.ProcessEvent(request, "sheet", "updated");

        if (processedEvent.EventIds == null || processedEvent.EventIds.Count == 0)
        {
            return new WebhookResponse<SheetResponse>
            {
                HttpResponseMessage = processedEvent.Response,
                Result = null
            };
        }

        var getRequest = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}");
        var sheetEntity = await Client.ExecuteWithErrorHandling<SheetEntity>(getRequest);

        return new WebhookResponse<SheetResponse>
        {
            HttpResponseMessage = processedEvent.Response,
            Result = new SheetResponse(sheetEntity)
        };
    }
}