using Apps.Smartsheet.Models.Entities.User;
using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Attachment;

public class AttachmentEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("createdBy")]
    public MinimalUserEntity CreatedBy { get; set; } = null!;

    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("mimeType")] 
    public string? MimeType { get; set; }
}