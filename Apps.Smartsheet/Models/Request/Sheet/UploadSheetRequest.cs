using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Smartsheet.Models.Request.Sheet;

public class UploadSheetRequest
{
    [Display("File")]
    public FileReference File { get; set; } = null!;

    [Display("Overwritten sheet name", Description = "Uses file name without its extension by default")]
    public string? OverwrittenSheetName { get; set; }
}