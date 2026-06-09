using Newtonsoft.Json;

namespace Apps.Smartsheet.Webhooks.Models.Entity;

public class WebhookPayloadEntity
{
    [JsonProperty("webhookId")]
    public long WebhookId { get; set; }

    [JsonProperty("events")] 
    public List<WebhookEventEntity> Events { get; set; } = [];
    
    [JsonProperty("challenge")]
    public string? Challenge { get; set; }
}