using Apps.Smartsheet.Models.Entities.Cell;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Cell;

public record CellResponse
{
    public CellResponse(CellEntity cell)
    {
        ColumnId = cell.ColumnId;
        Value = cell.Value;
        Type = cell.ColumnType;
        DisplayValue = cell.DisplayValue;
        Formula = cell.Formula;
    }

    [Display("Column ID")] 
    public string ColumnId { get; set; }

    [Display("Column value")]
    public string? Value { get; set; }

    [Display("Column type")]
    public string Type { get; set; }

    [Display("Column display value")]
    public string? DisplayValue { get; set; }

    [Display("Cell formula")]
    public string? Formula { get; set; }
}