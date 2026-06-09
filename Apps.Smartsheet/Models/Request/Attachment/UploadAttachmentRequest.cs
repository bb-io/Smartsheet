using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Smartsheet.Models.Request.Attachment;

public class UploadAttachmentRequest
{
    [Display("File")]
    public FileReference File { get; set; } = null!;
}