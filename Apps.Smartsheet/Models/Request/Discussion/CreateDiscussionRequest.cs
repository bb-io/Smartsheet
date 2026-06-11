using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Discussion;

public class CreateDiscussionRequest
{
    [Display("Discussion text")]
    public string DiscussionText { get; set; } = string.Empty;
}