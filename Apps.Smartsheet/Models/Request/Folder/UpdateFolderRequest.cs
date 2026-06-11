using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Folder;

public class UpdateFolderRequest
{
    [Display("New folder name")]
    public string FolderName { get; set; } = string.Empty;
}