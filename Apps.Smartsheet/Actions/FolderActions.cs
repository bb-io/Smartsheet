using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Models.Entities.Folder;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Response.Folder;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

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
}