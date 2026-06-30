namespace Apps.Smartsheet.Helper.Webhook;

public record ProcessedCellEvent(HttpResponseMessage Response, List<CellChange>? Changes)
{
    public bool ShouldPreflight => Changes is not { Count: > 0 };
}