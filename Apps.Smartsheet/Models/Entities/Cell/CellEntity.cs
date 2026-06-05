using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Cell;

public class CellEntity
{
    [JsonProperty("columnId")] 
    public string ColumnId { get; set; } = string.Empty;

    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("displayValue")]
    public string? DisplayValue { get; set; }

    [JsonProperty("formula")]
    public string? Formula { get; set; }
}