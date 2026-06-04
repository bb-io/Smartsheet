using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Helper.Validation;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Sheet;
using Apps.Smartsheet.Models.Response.File;
using Apps.Smartsheet.Models.Response.Sheet;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Sheets")]
public class SheetActions(InvocationContext context, IFileManagementClient fileManagementClient) 
    : SmartsheetInvocable(context)
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

    // https://developers.smartsheet.com/api/smartsheet/openapi/search/list-search
    [Action("Search within all sheets", Description = "Searches all sheets for the specified text")]
    public async Task<SearchWithinSheetsResponse> SearchWithinAllSheets( 
        [ActionParameter] SearchWithinSheetsRequest searchInput)
    {
        var request = new SmartsheetRequest("search");
        request.AddQueryParameter("query", searchInput.TextToSearch.Trim()); 
        request.AddQueryParameter("scopes", "cellData");
        
        var response = await Client.ExecuteWithErrorHandling<ResultsWrapper<SheetSearchEntity>>(request);
        var results = response.Results
            .Where(x => x.IsSheetRow)
            .Select(x => new SheetSearchResponse(x))
            .ToArray();

        return new(results);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/search/list-search-sheet
    [Action("Search within sheet", Description = "Searches a specific sheet for the specified text")]
    public async Task<SearchWithinSheetsResponse> SearchWithinSheet(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier,  // For the FF picker to work
        [ActionParameter] SearchWithinSheetsRequest searchInput)
    {
        var request = new SmartsheetRequest($"search/sheets/{sheetIdentifier.SheetId}");
        request.AddQueryParameter("query", searchInput.TextToSearch.Trim()); 
        request.AddQueryParameter("scopes", "cellData");
        
        var response = await Client.ExecuteWithErrorHandling<ResultsWrapper<SheetSearchEntity>>(request);
        var results = response.Results
            .Where(x => x.IsSheetRow)
            .Select(x => new SheetSearchResponse(x))
            .ToArray();

        return new(results);
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/getsheet
    [Action("Get sheet", Description = "Get a specific sheet")]
    public async Task<SheetResponse> GetSheet(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier  // For the FF picker to work)
    )
    {
        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}");
        var response = await Client.ExecuteWithErrorHandling<SheetEntity>(request);

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/create-sheet-in-workspace
    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/create-sheet-in-folder
    [Action("Create sheet", Description = "Create a sheet from scratch")]
    public async Task<CreatedSheetResponse> CreateSheet(
        [ActionParameter] WorkspaceIdentifier workspaceIdentifier,
        [ActionParameter] CreateSheetRequest createInput,
        [ActionParameter] OptionalFolderIdentifier folderIdentifier)
    {
        string endpoint = !string.IsNullOrEmpty(folderIdentifier.FolderId)
            ? $"folders/{folderIdentifier.FolderId}/sheets"
            : $"workspaces/{workspaceIdentifier.WorkspaceId}/sheets";
        
        var request = new SmartsheetRequest(endpoint, Method.Post)
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
        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<SheetEntity>>(request);

        return new(response.Result);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/updatesheet
    [Action("Update sheet", Description = "Update a specific sheet")]
    public async Task<CreatedSheetResponse> UpdateSheet(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier,  // For the FF picker to work
        [ActionParameter] UpdateSheetRequest updateInput)
    {
        var body = new Dictionary<string, string?>();
        
        if (!string.IsNullOrEmpty(updateInput.Name))
            body.Add("name", updateInput.Name);

        if (body.Count == 0)
            throw new PluginMisconfigurationException("Please specify at least one property to update");

        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}", Method.Put).AddJsonBody(body);
        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<SheetEntity>>(request);
        
        return new(response.Result);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/getsheet
    [Action("Download sheet", Description = "Download a specific sheet")]
    public async Task<FileResponse> DownloadSheet(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier,  // For the FF picker to work
        [ActionParameter] FileFormatIdentifier formatIdentifier,
        [ActionParameter] DownloadSheetRequest downloadInput)
    {
        string fileFormat = formatIdentifier.FileFormat ?? SheetFileFormats.Xlsx;

        var request = new SmartsheetRequest($"sheets/{sheetIdentifier.SheetId}")
            .AddHeader("Accept", fileFormat);
        var response = await Client.ExecuteWithErrorHandling(request);

        if (response.RawBytes is null)
            throw new PluginApplicationException("Failed to download a sheet");
        
        string rawName = string.IsNullOrEmpty(downloadInput.FileName) ? "Sheet" : downloadInput.FileName;
        string cleanName = Path.GetFileNameWithoutExtension(rawName);
        string fullFileName = $"{cleanName}{formatIdentifier.GetFileExtension()}";
        
        using var stream = new MemoryStream(response.RawBytes);
        var file = await fileManagementClient.UploadAsync(stream, fileFormat, fullFileName);
        return new(file);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/imports/import-sheet-into-folder
    // https://developers.smartsheet.com/api/smartsheet/openapi/imports/import-sheet-into-workspace
    [Action("Upload sheet", Description = "Upload a new sheet")]
    public async Task<CreatedSheetResponse> UploadSheet(
        [ActionParameter] WorkspaceIdentifier workspaceIdentifier,
        [ActionParameter] OptionalFolderIdentifier folderIdentifier,
        [ActionParameter] UploadSheetRequest uploadInput)
    {
        var inputFile = await fileManagementClient.DownloadAsync(uploadInput.File);
        var fileBytes = await inputFile.GetByteData();

        string sheetName = string.IsNullOrEmpty(uploadInput.OverwrittenSheetName)
            ? Path.GetFileNameWithoutExtension(uploadInput.File.Name)
            : uploadInput.OverwrittenSheetName;
        
        string endpoint = string.IsNullOrEmpty(folderIdentifier.FolderId)
            ? $"workspaces/{workspaceIdentifier.WorkspaceId}/sheets/import"
            : $"folders/{folderIdentifier.FolderId}/sheets/import";
        
        string dispositionHeaderValue = $"attachment; filename=\"{uploadInput.File.Name}\"";
        string contentType = uploadInput.File.GetSheetContentType();
        
        var request = new SmartsheetRequest(endpoint, Method.Post)
            .AddQueryParameter("sheetName", sheetName)
            .AddHeader("Content-Type", contentType)
            .AddHeader("Content-Disposition", dispositionHeaderValue)
            .AddParameter(contentType, fileBytes, ParameterType.RequestBody);

        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<SheetEntity>>(request);
        return new(response.Result);
    }
}