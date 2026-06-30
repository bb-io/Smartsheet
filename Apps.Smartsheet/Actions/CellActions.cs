using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Payload;
using Apps.Smartsheet.Models.Entities.Cell;
using Apps.Smartsheet.Models.Entities.Row;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Cell;
using Apps.Smartsheet.Models.Response.Cell;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Cells")]
public class CellActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/rows/row-get
    [Action("Get cell value", Description = "Get value of a specific cell")]
    public async Task<CellResponse> GetCell(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] RowIdentifier rowIdentifier,
        [ActionParameter] ColumnIdentifier columnIdentifier)
    {
        var cell = await FetchCell(sheetIdentifier.SheetId, rowIdentifier.RowId, columnIdentifier.ColumnId);
        return new(cell);
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/rows/update-rows
    [Action("Update cell value", Description = "Update value of an existing cell")]
    public async Task<CellResponse> UpdateCell(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] RowIdentifier rowIdentifier,
        [ActionParameter] ColumnIdentifier columnIdentifier,
        [ActionParameter] UpdateCellRequest updateRequest)
    {
        var body = new
        {
            id = long.Parse(rowIdentifier.RowId),
            cells = new List<object> { PayloadHelper.BuildRowPayload(columnIdentifier.ColumnId, updateRequest.NewValue) }
        };

        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/rows", Method.Put)
            .WithJsonBody(body);
        await Client.ExecuteWithErrorHandling<Result<RowEntity[]>>(request);

        // The update response does not return values for multiselect cells
        // So we need to refetch the cell
        var updatedCell = await FetchCell(sheetIdentifier.SheetId, rowIdentifier.RowId, columnIdentifier.ColumnId);
        return new(updatedCell);
    }

    private async Task<CellEntity> FetchCell(string sheetId, string rowId, string columnId)
    {
        var request = new SmartsheetRequest($"sheets/{sheetId}/rows/{rowId}")
            .AddQueryParameter("include", "columns,columnType");
        var response = await Client.ExecuteWithErrorHandling<RowEntity>(request);
        
        var cell = response.Cells.First(x => x.ColumnId == columnId);
        return cell;
    }
}