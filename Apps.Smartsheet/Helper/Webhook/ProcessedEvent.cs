namespace Apps.Smartsheet.Helper.Webhook;

public record ProcessedEvent(HttpResponseMessage Response, List<string>? EventIds);