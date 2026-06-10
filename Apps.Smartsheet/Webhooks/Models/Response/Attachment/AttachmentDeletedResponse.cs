using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Webhooks.Models.Response.Attachment;

public record AttachmentDeletedResponse([Display("Attachment IDs")] string[] Ids);