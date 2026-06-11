using Apps.Smartsheet.Models.Entities.Row;
using Apps.Smartsheet.Models.Response.Cell;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Row;

public record RowResponse
{
    public RowResponse(RowEntity row)
    {
        Id = row.Id;
        RowNumber = row.RowNumber;
        Cells = row.Cells.Select(x => new CellResponse(x)).ToList();
        CreatedAt = row.CreatedAt;
        ModifiedAt = row.ModifiedAt;
    }

    [Display("Row ID")]
    public string Id { get; set; }

    [Display("Row number")]
    public int RowNumber { get; set; }

    [Display("Cells")]
    public List<CellResponse> Cells { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Modified at")]
    public DateTime? ModifiedAt { get; set; }
}