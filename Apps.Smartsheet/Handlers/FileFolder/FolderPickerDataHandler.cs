using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;

namespace Apps.Smartsheet.Handlers.FileFolder;

public class FolderPickerDataHandler(
    InvocationContext context,
    [ActionParameter] WorkspaceIdentifier workspaceIdentifier)
    : BasePicker(context, workspaceIdentifier.WorkspaceId), IAsyncFileDataSourceItemHandler
{
    public async Task<IEnumerable<FolderPathItem>> GetFolderPathAsync(
        FolderPathDataSourceContext context, 
        CancellationToken cancellationToken)
    {
        return await base.GetFolderPathAsync(context.FileDataItemId);
    }

    public async Task<IEnumerable<FileDataItem>> GetFolderContentAsync(
        FolderContentDataSourceContext context, 
        CancellationToken cancellationToken)
    {
        return await base.GetFolderContentAsync(context.FolderId, cancellationToken, ResourceTypes.Folder);
    }
}