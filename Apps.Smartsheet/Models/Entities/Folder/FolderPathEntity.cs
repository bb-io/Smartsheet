using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Folder;

public class FolderPathEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("folders")]
    public List<FolderPathEntity>? Folders { get; set; }
}