using Apps.Smartsheet.Models.Entities.Comment;
using Apps.Smartsheet.Models.Entities.User;
using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Discussion;

public class DiscussionEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("commentCount")]
    public int CommentCount { get; set; }

    [JsonProperty("createdBy")]
    public MinimalUserEntity CreatedBy { get; set; } = null!;

    [JsonProperty("lastCommentedAt")]
    public DateTime LastCommentedAt { get; set; }
    
    [JsonProperty("lastCommentedUser")]
    public MinimalUserEntity? LastCommentedUser { get; set; }

    [JsonProperty("comments")]
    public List<CommentEntity> Comments { get; set; } = [];

    public override string ToString()
    {
        return $"{Title} ({CommentCount} {(CommentCount == 1 ? "comment" : "comments")})";
    }
}