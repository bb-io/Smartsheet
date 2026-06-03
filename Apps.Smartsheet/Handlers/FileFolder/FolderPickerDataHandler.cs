using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Models.Entities.Children;
using Apps.Smartsheet.Models.Entities.Folder;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;
using File = Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems.File;

namespace Apps.Smartsheet.Handlers.FileFolder;

public class FolderPickerDataHandler : SmartsheetInvocable, IAsyncFileDataSourceItemHandler
{
    private readonly string _workspaceId;
    
    public FolderPickerDataHandler(
        InvocationContext context, 
        [ActionParameter] WorkspaceIdentifier workspaceIdentifier) 
        : base(context)
    {
        if (string.IsNullOrWhiteSpace(workspaceIdentifier.WorkspaceId))
            throw new PluginMisconfigurationException("Please specify a workspace ID first");

        _workspaceId = workspaceIdentifier.WorkspaceId;
    }

    public async Task<IEnumerable<FolderPathItem>> GetFolderPathAsync(
        FolderPathDataSourceContext context, 
        CancellationToken ct)
    {
        if (string.IsNullOrEmpty(context.FileDataItemId))
            return [];

        var request = new SmartsheetRequest($"folders/{context.FileDataItemId}/path");
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

    public async Task<IEnumerable<FileDataItem>> GetFolderContentAsync(
        FolderContentDataSourceContext context, 
        CancellationToken ct)
    {
        string endpoint = string.IsNullOrEmpty(context.FolderId)
            ? $"workspaces/{_workspaceId}/children"
            : $"folders/{context.FolderId}/children";
        
        var request = new SmartsheetRequest(endpoint);
        var response = await Client.PaginateToken<ChildEntity>(request, timesToPaginate: 2).ToArrayAsync(ct);
            
        var folders = response
            .Where(x => x.IsFolder)
            .Select(x => new Folder
            {
                Id = x.Id,
                DisplayName = x.Name,
                Date = x.ModifiedAt ?? x.CreatedAt,
                IsSelectable = true
            })
            .ToList();

        var otherItems = response
            .Where(x => !x.IsFolder)
            .Select(x => new File
            {
                Id = x.Id,
                DisplayName = x.Name,
                Date = x.ModifiedAt ?? x.CreatedAt,
                IsSelectable = false
            })
            .ToList();

        var finalItems = new List<FileDataItem>();
        finalItems.AddRange(folders);
        finalItems.AddRange(otherItems);

        return finalItems;
    }
}