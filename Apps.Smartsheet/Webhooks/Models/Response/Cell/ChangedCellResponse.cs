using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Webhooks.Models.Response.Cell;

public record ChangedCellResponse
{
    [Display("Row ID")]
    public string RowId { get; set; } = string.Empty;

    [Display("Row number")]
    public int RowNumber { get; set; }

    [Display("Column ID")]
    public string ColumnId { get; set; } = string.Empty;

    [Display("Column type")]
    public string Type { get; set; } = string.Empty;

    [Display("Value")]
    public string? Value { get; set; }

    [Display("Display value")]
    public string? DisplayValue { get; set; }
}