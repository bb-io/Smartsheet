using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Sheet;

public class UpdateSheetRequest
{
    [Display("Sheet name")]
    public string? Name { get; set; }
}