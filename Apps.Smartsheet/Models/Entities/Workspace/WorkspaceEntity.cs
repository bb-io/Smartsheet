using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Workspace;

public class WorkspaceEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("accessLevel")]
    public string AccessLevel { get; set; } = string.Empty;

    [JsonProperty("permalink")]
    public string Permalink { get; set; } = string.Empty;
}