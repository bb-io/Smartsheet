using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Column;

public class ColumnEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("index")]
    public int Index { get; set; }

    [JsonProperty("formula")]
    public string? Formula { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("hidden")]
    public bool IsHidden { get; set; }

    [JsonProperty("locked")]
    public bool IsLocked { get; set; }

    [JsonProperty("lockedForUser")]
    public bool IsLockedForRequestingUser { get; set; }

    [JsonProperty("primary")]
    public bool? IsPrimary { get; set; }

    public override string ToString()
    {
        return $"{Title} (Type: {Type}{(IsLocked ? ", locked" : "")})";
    }
}