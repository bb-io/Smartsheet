using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Polling.Models.Request.Folder;

public class FoldersCreatedRequest
{
    [Display("Folder name contains")]
    public string? NameContains { get; set; }
}