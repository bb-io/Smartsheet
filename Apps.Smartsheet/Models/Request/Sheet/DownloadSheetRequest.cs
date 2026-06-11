using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Sheet;

public class DownloadSheetRequest
{
    [Display("File name")]
    public string? FileName { get; set; }
}