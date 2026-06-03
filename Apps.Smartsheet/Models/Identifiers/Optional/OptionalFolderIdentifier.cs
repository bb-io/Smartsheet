using Apps.Smartsheet.Handlers.FileFolder;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;

namespace Apps.Smartsheet.Models.Identifiers.Optional;

public class OptionalFolderIdentifier
{
    [Display("Folder ID"), FileDataSource(typeof(FolderPickerDataHandler))]
    public string? FolderId { get; set; }
}