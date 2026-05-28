using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Identifiers;

public class WorkspaceIdentifier
{
    [Display("Workspace ID")]
    public string WorkspaceId { get; set; } = string.Empty;
}