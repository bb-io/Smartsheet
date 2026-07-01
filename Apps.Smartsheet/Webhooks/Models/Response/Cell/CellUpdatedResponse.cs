namespace Apps.Smartsheet.Webhooks.Models.Response.Cell;

public record CellUpdatedResponse(List<ChangedCellResponse> Cells);