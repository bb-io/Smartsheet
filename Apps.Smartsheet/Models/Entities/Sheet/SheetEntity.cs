using Apps.Smartsheet.Models.Entities.Row;
using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Sheet;

public class SheetEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("accessLevel")]
    public string AccessLevel { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("modifiedAt")]
    public DateTime ModifiedAt { get; set; }

    [JsonProperty("permalink")]
    public string Permalink { get; set; } = string.Empty;

    [JsonProperty("rows")] 
    public List<RowEntity> Rows { get; set; } = [];
}