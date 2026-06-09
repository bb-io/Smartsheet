using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class CommentIdentifier
{
    [Display("Comment ID"), DataSource(typeof(CommentDataHandler))]
    public string CommentId { get; set; } = string.Empty;

    [Display("Discussion ID"), DataSource(typeof(DiscussionDataHandler))]
    public string? DiscussionId { get; set; }
}