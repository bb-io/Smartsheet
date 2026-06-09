using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers.Optional;

public class OptionalCommentIdentifier
{
    [Display("Comment ID"), DataSource(typeof(CommentDataHandler))]
    public string? CommentId { get; set; }
}