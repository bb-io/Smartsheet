using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Helper.Error;
using Apps.Smartsheet.Models.Entities.Column;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Column;
using Apps.Smartsheet.Models.Response.Column;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Columns")]
public class ColumnActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/columns/columns-listonsheet
    [Action("Search columns", Description = "Search columns for a specific sheet")]
    public async Task<SearchColumnsResponse> SearchColumns(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] SearchColumnsRequest searchInput)
    {
        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/columns");
        var response = await Client.PaginateToken<ColumnEntity>(request)
            .WhereContains(x => x.Title, searchInput.ColumnTitleContains)
            .Select(x => new ColumnResponse(x))
            .ToArrayAsync();

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/columns/column-get
    [Action("Get column", Description = "Get definition for a specific column")]
    public async Task<ColumnResponse> GetColumn(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] ColumnIdentifier columnIdentifier)
    {
        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/columns/{columnIdentifier.ColumnId}");
        var response = await Client.ExecuteWithErrorHandling<ColumnEntity>(request);

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/columns/columns-addtosheet
    [Action("Add column", Description = "Insert one column into a specific sheet")]
    public async Task<ColumnResponse> AddColumn(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] AddColumnRequest addInput)
    {
        var body = new Dictionary<string, object?>
        {
            { "title", addInput.Title },
            { "index", addInput.Index },
            { "type", addInput.Type },
        };
        body.AddIfNotEmpty("description", addInput.Description);
        body.AddIfNotEmpty("hidden", addInput.IsHidden);
        body.AddIfNotEmpty("locked", addInput.IsLocked);
        body.AddIfNotEmpty("width", addInput.Width);

        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}/columns", Method.Post)
            .WithJsonBody(body);
        var response = await Client.ExecuteWithErrorHandling<Result<ColumnEntity>>(request);

        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/columns/column-updatecolumn
    [Action("Update column", Description = "Update properties of a specific column")]
    public async Task<ColumnResponse> UpdateColumn(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] ColumnIdentifier columnIdentifier,
        [ActionParameter] UpdateColumnRequest updateInput)
    {
        var body = new Dictionary<string, object?>();
        body.AddIfNotEmpty("title", updateInput.Title);
        body.AddIfNotEmpty("type", updateInput.Type);
        body.AddIfNotEmpty("description", updateInput.Description);
        body.AddIfNotEmpty("index", updateInput.Index);
        body.AddIfNotEmpty("formula", updateInput.Formula);
        body.AddIfNotEmpty("width", updateInput.Width);
        body.AddIfNotEmpty("hidden", updateInput.IsHidden);
        body.AddIfNotEmpty("locked", updateInput.IsLocked);

        string endpoint = $"sheets/{sheetIdentifier.SheetId}/columns/{columnIdentifier.ColumnId}";
        var request = new SmartsheetRequest(endpoint, Method.Put)
            .AddJsonBody(body);
        var response = await Client.ExecuteWithErrorHandling<Result<ColumnEntity>>(request);

        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/columns/column-delete
    [Action("Delete column", Description = "Delete a specific column")]
    public async Task DeleteColumn(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] ColumnIdentifier columnIdentifier)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/columns/{columnIdentifier.ColumnId}";
        var request = new SmartsheetRequest(endpoint, Method.Delete);
        var response = await Client.ExecuteWithErrorHandling<Result>(request);
        
        if (!response.IsSuccessfulResponse)
            throw new PluginApplicationException(ErrorMessageHelper.GenerateFailedToDeleteMessage("column"));
    }
}