using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Polling.Models.Request.Sheet;

public class SheetsCreatedRequest
{
    [Display("Sheet name contains")]
    public string? NameContains { get; set; }
}