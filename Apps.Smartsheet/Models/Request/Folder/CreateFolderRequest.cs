using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Folder;

public class CreateFolderRequest
{
    [Display("Folder name")]
    public string FolderName { get; set; } = string.Empty;
}