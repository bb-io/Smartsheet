namespace Apps.Smartsheet.Helper.Webhook;

public record ProcessedEvent(HttpResponseMessage Response, List<string>? EventIds)
{
    public bool ShouldPreflight => EventIds is not { Count: > 0 };
}