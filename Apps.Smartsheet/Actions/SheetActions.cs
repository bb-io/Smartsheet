using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Helper.Validation;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Sheet;
using Apps.Smartsheet.Models.Response.Sheet;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Sheets")]
public class SheetActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/list-sheets
    [Action("Search sheets", Description = "Search sheets that the user has access to")]
    public async Task<SearchSheetsResponse> SearchSheets([ActionParameter] SearchSheetsRequest searchInput)
    {
        searchInput.ValidateDates();
        
        var request = new SmartsheetRequest("sheets");
        var response = await Client.PaginateOffset<SheetEntity>(request)
            .WhereContains(x => x.Name, searchInput.NameContains)
            .ApplyCreatedDateFilter(x => x.CreatedAt, searchInput)
            .ApplyModifiedDateFilter(x => x.ModifiedAt, searchInput)
            .Select(x => new SheetResponse(x))
            .ToArrayAsync();

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/getsheet
    [Action("Get sheet", Description = "Get a specific sheet")]
    public async Task<SheetResponse> GetSheet([ActionParameter] SheetIdentifier sheetIdentifier)
    {
        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}");
        var response = await Client.ExecuteWithErrorHandling<SheetEntity>(request);

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/create-sheet-in-workspace
    [Action("Create sheet in workspace", Description = "Create a sheet from scratch in the specified workspace")]
    public async Task<SheetResponse> CreateSheetInWorkspace(
        [ActionParameter] WorkspaceIdentifier workspaceIdentifier,
        [ActionParameter] CreateSheetInWorkspaceRequest createInput)
    {
        var request = new SmartsheetRequest($"workspaces/{workspaceIdentifier.WorkspaceId}/sheets", Method.Post)
            .WithJsonBody(new
            {
                name = createInput.Name,
                columns = new[]
                {
                    new 
                    {
                        title = "New column",
                        primary = true,
                        type = "TEXT_NUMBER"
                    }
                }
            });
        var response = await Client.ExecuteWithErrorHandling<SheetEntity>(request);

        return new(response);
    }
}