using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Entities.Children;
using Apps.Smartsheet.Models.Entities.Folder;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;
using File = Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems.File;

namespace Apps.Smartsheet.Handlers.FileFolder;

public class BasePicker : SmartsheetInvocable
{
    private readonly string _workspaceId;
    
    protected BasePicker(InvocationContext context, string? workspaceIdentifier) : base(context)
    {
        if (string.IsNullOrWhiteSpace(workspaceIdentifier))
            throw new PluginMisconfigurationException("Please specify a workspace ID first");

        _workspaceId = workspaceIdentifier;
    }

    protected async Task<IEnumerable<FolderPathItem>> GetFolderPathAsync(string? fileDataItemId)
    {
        if (string.IsNullOrEmpty(fileDataItemId))
            return [];

        var request = new SmartsheetRequest($"folders/{fileDataItemId}/path");
        var response = await Client.ExecuteWithErrorHandling<FolderPathEntity>(request);

        var breadcrumbs = new List<FolderPathItem>();
        var currentNode = response;

        while (currentNode != null)
        {
            breadcrumbs.Add(new FolderPathItem { Id = currentNode.Id, DisplayName = currentNode.Name });
            currentNode = currentNode.Folders?.FirstOrDefault();
        }
        
        if (breadcrumbs.Count > 1)
            breadcrumbs.RemoveAt(breadcrumbs.Count - 1);

        return breadcrumbs;
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/folders/get-folder-children
    // https://developers.smartsheet.com/api/smartsheet/openapi/workspaces/get-workspace-children
    protected async Task<IEnumerable<FileDataItem>> GetFolderContentAsync(
        string? folderId,
        CancellationToken cancellationToken,
        params string[] selectableTypes)
    {
        string endpoint = string.IsNullOrEmpty(folderId)
            ? $"workspaces/{_workspaceId}/children"
            : $"folders/{folderId}/children";
        
        var request = new SmartsheetRequest(endpoint);
        var response = await Client.PaginateToken<ChildEntity>(request, timesToPaginate: 2).ToArrayAsync(cancellationToken);
            
        var folders = response
            .Where(x => x.IsFolder)
            .Select(x => new Folder
            {
                Id = x.Id,
                DisplayName = x.Name,
                Date = x.ModifiedAt ?? x.CreatedAt,
                IsSelectable = selectableTypes.Contains(ResourceTypes.Folder)
            })
            .ToList();

        var otherItems = response
            .Where(x => !x.IsFolder)
            .Select(x => new File
            {
                Id = x.Id,
                DisplayName = x.Name,
                Date = x.ModifiedAt ?? x.CreatedAt,
                IsSelectable = selectableTypes.Contains(x.ResourceType)
            })
            .ToList();

        var finalItems = new List<FileDataItem>();
        finalItems.AddRange(folders);
        finalItems.AddRange(otherItems);

        return finalItems;
    }
}