using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class DiscussionIdentifier
{
    [Display("Discussion ID"), DataSource(typeof(DiscussionDataHandler))]
    public string DiscussionId { get; set; } = string.Empty;
}