using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class WorkspaceIdentifier
{
    [Display("Workspace ID"), DataSource(typeof(WorkspaceDataHandler))]
    public string WorkspaceId { get; set; } = string.Empty;
}