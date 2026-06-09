using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers.Optional;

public class OptionalDiscussionIdentifier
{
    [Display("Discussion ID"), DataSource(typeof(DiscussionDataHandler))]
    public string? DiscussionId { get; set; }
}