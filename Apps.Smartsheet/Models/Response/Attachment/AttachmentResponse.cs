using Apps.Smartsheet.Models.Entities.Attachment;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Attachment;

public record AttachmentResponse
{
    public AttachmentResponse(AttachmentEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        CreatedAt = entity.CreatedAt;
        CreatedByUserName = entity.CreatedBy.Name;
        CreatedByUserEmail = entity.CreatedBy.Email;
    }

    [Display("Attachment ID")]
    public string Id { get; set; }

    [Display("Attachment name")]
    public string Name { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Created by user name")]
    public string CreatedByUserName { get; set; }

    [Display("Created by user email")]
    public string CreatedByUserEmail { get; set; }
}