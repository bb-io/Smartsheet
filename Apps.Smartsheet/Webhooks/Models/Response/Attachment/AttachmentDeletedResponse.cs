using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Webhooks.Models.Response.Attachment;

public record AttachmentDeletedResponse(string[] Ids)
{
    [Display("Attachment IDs")] 
    public string[] Ids { get; set; } = Ids;
}