using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Comment;

public class UpdateCommentRequest
{
    [Display("Comment text")]
    public string CommentText { get; set; } = string.Empty;
}