using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers.Optional;

public class OptionalAttachmentIdentifier
{
    [Display("Attachment ID"), DataSource(typeof(AttachmentDataHandler))]
    public string? AttachmentId { get; set; }
}