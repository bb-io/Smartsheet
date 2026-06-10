using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Helper.Polling;
using Apps.Smartsheet.Models.Entities.Children;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Response.Folder;
using Apps.Smartsheet.Polling.Models.Memory;
using Apps.Smartsheet.Polling.Models.Request.Folder;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.Smartsheet.Polling;

[PollingEventList("Folders")]
public class FolderPollingList(InvocationContext context) : SmartsheetInvocable(context)
{
    [PollingEvent("On folders created", Description = "Triggers when new folders are created")]
    public async Task<PollingEventResponse<DateMemory, SearchFoldersResponse>> OnFoldersCreated(
        PollingEventRequest<DateMemory> pollingRequest,
        [PollingEventParameter] WorkspaceIdentifier workspaceIdentifier,
        [PollingEventParameter] OptionalFolderIdentifier folderIdentifier,
        [PollingEventParameter] FoldersCreatedRequest createInput)
    {
        var interactionTimestamp = DateTime.UtcNow;
        
        if (pollingRequest.Memory?.LastInteraction is null)
            return PollingHelper.DontFlyBird<SearchFoldersResponse>(interactionTimestamp);
        
        string endpoint = string.IsNullOrEmpty(folderIdentifier.FolderId)
            ? $"workspaces/{workspaceIdentifier.WorkspaceId}/children"
            : $"folders/{folderIdentifier.FolderId}/children";
        
        var request = new SmartsheetRequest(endpoint);
        var response = await Client.PaginateToken<ChildEntity>(request)
            .Where(x => x.IsFolder)
            .Where(x => x.CreatedAt > pollingRequest.Memory.LastInteraction.Value)
            .WhereContains(x => x.Name, createInput.NameContains)
            .Select(x => new FolderResponse(x))
            .ToArrayAsync();
        
        return response.Length == 0 
            ? PollingHelper.DontFlyBird<SearchFoldersResponse>(interactionTimestamp) 
            : PollingHelper.FlyBird<SearchFoldersResponse>(new(response), interactionTimestamp);
    }
}