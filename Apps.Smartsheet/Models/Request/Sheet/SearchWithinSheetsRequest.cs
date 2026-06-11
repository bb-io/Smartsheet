using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Sheet;

public class SearchWithinSheetsRequest
{
    [Display("Text to search")]
    public string TextToSearch { get; set; } = string.Empty;
}