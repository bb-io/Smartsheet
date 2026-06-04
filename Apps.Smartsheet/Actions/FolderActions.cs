using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Folder;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Folder;
using Apps.Smartsheet.Models.Response.Folder;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Folders")]
public class FolderActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/folders/get-folder-metadata
    [Action("Get folder", Description = "Get metadata for a specific folder")]
    public async Task<FolderResponse> GetFolder(
        [ActionParameter] FolderIdentifier folderIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier   // For the FF picker to work
        )
    {
        var request = new SmartsheetRequest($"folders/{folderIdentifier.FolderId}/metadata");
        var response = await Client.ExecuteWithErrorHandling<FolderEntity>(request);
        
        return new(response);
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/folders/get-folder-path
    [Action("Get folder path", Description = "Get path for a specific folder")]
    public async Task<FolderPathResponse> GetFolderPath(
        [ActionParameter] FolderIdentifier folderIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier   // For the FF picker to work
    )
    {
        var request = new SmartsheetRequest($"folders/{folderIdentifier.FolderId}/path");
        var response = await Client.ExecuteWithErrorHandling<FolderPathEntity>(request);

        var pathEntities = response.GetPathEntities();
        var folderNames = pathEntities.Select(x => x.Name);
        
        string folderPath = string.Join('/', folderNames);
        return new(folderPath);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/folders/create-folder-folder
    // https://developers.smartsheet.com/api/smartsheet/openapi/workspaces/create-workspace-folder
    [Action("Create folder", Description = "Create a new folder")]
    public async Task<CreatedFolderResponse> CreateFolder(
        [ActionParameter] WorkspaceIdentifier workspaceIdentifier,
        [ActionParameter] OptionalFolderIdentifier folderIdentifier,
        [ActionParameter] CreateFolderRequest createInput)
    {
        string endpoint = string.IsNullOrEmpty(folderIdentifier.FolderId)
            ? $"workspaces/{workspaceIdentifier.WorkspaceId}/folders"
            : $"folders/{folderIdentifier.FolderId}/folders";

        var request = new SmartsheetRequest(endpoint, Method.Post)
            .WithJsonBody(new { name = createInput.FolderName });
        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<FolderEntity>>(request);
        
        return new(response.Result);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/folders/update-folder
    [Action("Update folder", Description = "Update an existing folder")]
    public async Task<CreatedFolderResponse> UpdateFolder(
        [ActionParameter] FolderIdentifier folderIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier,   // For the FF picker to work
        [ActionParameter] UpdateFolderRequest updateInput)
    {
        var request = new SmartsheetRequest($"folders/{folderIdentifier.FolderId}", Method.Put)
            .WithJsonBody(new { name = updateInput.FolderName });
        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<FolderEntity>>(request);
        
        return new(response.Result);
    }

    [Action("Delete folder", Description = "Delete an existing folder")]
    public async Task DeleteFolder(
        [ActionParameter] FolderIdentifier folderIdentifier,
        [ActionParameter] OptionalWorkspaceIdentifier workspaceIdentifier   // For the FF picker to work
    )
    {
        var request = new SmartsheetRequest($"folders/{folderIdentifier.FolderId}", Method.Delete);
        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<FolderEntity>>(request);

        if (!response.IsSuccessfulResponse)
            throw new PluginApplicationException(
                "Failed to delete a folder. No additional information received from Smartsheet");
    }
}