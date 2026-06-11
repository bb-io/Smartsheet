using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Sheet;

public class CreateSheetRequest
{
    [Display("Sheet name")]
    public string Name { get; set; } = string.Empty;
}