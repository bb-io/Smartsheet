using Newtonsoft.Json;

namespace Apps.Smartsheet.Webhooks.Models.Entity;

public class WebhookEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("callbackUrl")]
    public string CallbackUrl { get; set; } = string.Empty;
}