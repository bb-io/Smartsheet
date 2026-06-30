using Apps.Smartsheet.Models.Response.Row;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Webhooks.Models.Response.Cell;

public record CellUpdatedResponse(List<RowResponse> Rows)
{
    [Display("Changed rows")]
    public List<RowResponse> Rows { get; set; } = Rows;
}