using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Children;

public class ChildEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("accessLevel")]
    public string AccessLevel { get; set; } = string.Empty;

    [JsonProperty("permalink")]
    public string Permalink { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("modifiedAt")]
    public DateTime? ModifiedAt { get; set; }

    [JsonProperty("resourceType")]
    public string ResourceType { get; set; } = string.Empty;

    public bool IsFolder => ResourceType == "folder";
}