using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Payload;
using Apps.Smartsheet.Models.Entities.Row;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Row;
using Apps.Smartsheet.Models.Response.Row;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Rows")]
public class RowActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/rows/row-get
    [Action("Get row", Description = "Get row values")]
    public async Task<RowResponse> GetRow(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] RowIdentifier rowIdentifier)
    {
        var row = await FetchRow(sheetIdentifier.SheetId, rowIdentifier.RowId);
        return new(row);
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/rows/rows-addtosheet
    [Action("Add row", Description = "Insert a new row into a specific sheet")]
    public async Task<RowResponse> AddRow(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] AddRowRequest addInput)
    {
        addInput.Validate();
        
        var body = new
        {
            toTop = addInput.AppendToTop ?? false,
            cells = PayloadHelper.BuildRowPayload(addInput.ColumnIds, addInput.ColumnValues)
        };

        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/rows", Method.Post)
            .WithJsonBody(body);
        var response = await Client.ExecuteWithErrorHandling<Result<RowEntity>>(request);

        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/rows/update-rows
    [Action("Update row", Description = "Update values of an existing row")]
    public async Task<RowResponse> UpdateRow(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] RowIdentifier rowIdentifier,
        [ActionParameter] UpdateRowRequest updateRequest)
    {
        updateRequest.Validate();
        
        var body = new
        {
            id = rowIdentifier.RowId,
            cells = PayloadHelper.BuildRowPayload(updateRequest.ColumnIds, updateRequest.ColumnValues)
        };

        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/rows", Method.Put)
            .WithJsonBody(body);
        var response = await Client.ExecuteWithErrorHandling<Result<RowEntity[]>>(request);

        // The update response does not return values for multiselect cells
        // So we need to refetch the row
        string rowId = response.Value.First().Id;
        var completeRow = await FetchRow(sheetIdentifier.SheetId, rowId);
        return new(completeRow);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/rows/delete-rows
    [Action("Delete row", Description = "Delete a specific row")]
    public async Task DeleteRow(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] RowIdentifier rowIdentifier)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/rows?ids={rowIdentifier.RowId}";
        var request = new SmartsheetRequest(endpoint, Method.Delete);
        var response = await Client.ExecuteWithErrorHandling<Result>(request);
        
        if (!response.IsSuccessfulResponse)
            throw new PluginApplicationException(
                "Failed to delete a row. No additional information received from Smartsheet");
    }

    private async Task<RowEntity> FetchRow(string sheetId, string rowId)
    {
        var request = new SmartsheetRequest($"sheets/{sheetId}/rows/{rowId}")
            .AddQueryParameter("include", "columns");
        return await Client.ExecuteWithErrorHandling<RowEntity>(request);
    }
}