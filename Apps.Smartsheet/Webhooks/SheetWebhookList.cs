using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Webhook;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Response.Row;
using Apps.Smartsheet.Models.Response.Sheet;
using Apps.Smartsheet.Webhooks.Handlers;
using Apps.Smartsheet.Webhooks.Models.Response.Cell;
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

    [Webhook("On cell updated", typeof(SmartsheetSheetEventHandler), Description = "Triggers when a cell is updated")]
    public async Task<WebhookResponse<CellUpdatedResponse>> OnCellUpdated(
        WebhookRequest request,
        [WebhookParameter(true)] SheetIdentifier sheetIdentifier,
        [WebhookParameter] OptionalRowIdentifier? rowIdentifier)
    {
        var processed = WebhookHelper.ProcessCellEvents(request, "updated");
        if (processed.ShouldPreflight)
            return WebhookHelper.Preflight<CellUpdatedResponse>(processed.Response);

        var changes = processed.Changes!;
        if (!string.IsNullOrWhiteSpace(rowIdentifier?.RowId))
            changes = changes.Where(c => c.RowId == rowIdentifier.RowId).ToList();

        if (changes.Count == 0)
            return WebhookHelper.Preflight<CellUpdatedResponse>(processed.Response);

        var rowIds = changes
            .Where(c => !string.IsNullOrEmpty(c.RowId))
            .Select(c => c.RowId!)
            .Distinct()
            .ToList();

        var getRequest = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}?rowIds={string.Join(",", rowIds)}");
        var sheetEntity = await Client.ExecuteWithErrorHandling<SheetEntity>(getRequest);
        
        var changedRows = sheetEntity.Rows.Select(r => new RowResponse(r)).ToList();
        return new WebhookResponse<CellUpdatedResponse>
        {
            HttpResponseMessage = processed.Response,
            Result = new CellUpdatedResponse(changedRows)
        };
    }
}