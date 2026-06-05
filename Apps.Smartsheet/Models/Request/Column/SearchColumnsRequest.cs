using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Column;

public class SearchColumnsRequest
{
    [Display("Column title contains")]
    public string? ColumnTitleContains { get; set; }
}