using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Sheet;

public class SheetSearchEntity
{
    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;

    [JsonProperty("parentObjectType")]
    public string ParentObjectType { get; set; } = string.Empty;
    
    [JsonProperty("parentObjectId")]
    public string ParentObjectId { get; set; } = string.Empty;

    [JsonProperty("objectType")]
    public string ObjectType { get; set; } = string.Empty;
    
    [JsonProperty("objectId")]
    public string ObjectId { get; set; } = string.Empty;

    [JsonProperty("favorite")]
    public bool IsFavorite { get; set; }

    public bool IsSheetRow => ParentObjectType == "sheet" && ObjectType == "row";
}