using Newtonsoft.Json;

namespace Apps.Smartsheet.Webhooks.Models.Entity;

public class WebhookEventEntity
{
    [JsonProperty("id")] 
    public string Id { get; set; } = string.Empty;
    
    [JsonProperty("eventType")]
    public string EventType { get; set; } = string.Empty;
    
    [JsonProperty("objectType")]
    public string ObjectType { get; set; } = string.Empty;
}