using Apps.Smartsheet.Models.Entities.User;
using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Comment;

public class CommentEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("modifiedAt")]
    public DateTime ModifiedAt { get; set; }
    
    [JsonProperty("createdBy")]
    public MinimalUserEntity CreatedBy { get; set; } = null!;
}