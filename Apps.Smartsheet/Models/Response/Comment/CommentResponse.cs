using Apps.Smartsheet.Models.Entities.Comment;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Comment;

public record CommentResponse
{
    public CommentResponse(CommentEntity entity)
    {
        Id = entity.Id;
        Text = entity.Text;
        CreatedAt = entity.CreatedAt;
        ModifiedAt = entity.ModifiedAt;
        CreatedByUserName = entity.CreatedBy.Name;
        CreatedByUserEmail = entity.CreatedBy.Email;
    }

    [Display("Comment ID")]
    public string Id { get; set; }

    [Display("Comment text")]
    public string Text { get; set; }

    [Display("Created at")] 
    public DateTime CreatedAt { get; set; }
    
    [Display("Modified at")]
    public DateTime ModifiedAt { get; set; }

    [Display("Created by user name")] 
    public string CreatedByUserName { get; set; }
    
    [Display("Created by user email")] 
    public string CreatedByUserEmail { get; set; }
}