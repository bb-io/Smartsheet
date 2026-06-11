using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class AttachmentIdentifier
{
    [Display("Attachment ID"), DataSource(typeof(AttachmentDataHandler))]
    public string AttachmentId { get; set; } = string.Empty;
}