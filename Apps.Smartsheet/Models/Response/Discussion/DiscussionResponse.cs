using Apps.Smartsheet.Models.Entities.Discussion;
using Apps.Smartsheet.Models.Response.Comment;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Discussion;

public record DiscussionResponse
{
    public DiscussionResponse(DiscussionEntity entity)
    {
        Id = entity.Id;
        Title = entity.Title;
        CommentCount = entity.CommentCount;
        CreatedByUserName = entity.CreatedBy.Name;
        CreatedByUserEmail = entity.CreatedBy.Email;
        LastCommentedAt = entity.LastCommentedAt;
        LastCommentedUserName = entity.LastCommentedUser?.Name;
        LastCommentedUserEmail = entity.LastCommentedUser?.Email;
        Comments = entity.Comments.Select(x => new CommentResponse(x)).ToList();
    }

    [Display("Discussion ID")]
    public string Id { get; set; }

    [Display("Discussion title")]
    public string Title { get; set; }
    
    [Display("Comment count")] 
    public int CommentCount { get; set; }

    [Display("Created by user name")] 
    public string CreatedByUserName { get; set; }
    
    [Display("Created by user email")] 
    public string CreatedByUserEmail { get; set; }

    [Display("Last commented at")]
    public DateTime LastCommentedAt { get; set; }
    
    [Display("Last commented user name")] 
    public string? LastCommentedUserName { get; set; }
    
    [Display("Last commented user email")] 
    public string? LastCommentedUserEmail { get; set; }

    [Display("Comments")]
    public List<CommentResponse> Comments { get; set; }
}