using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers.Optional;

public class OptionalWorkspaceIdentifier
{
    [Display("Workspace ID"), DataSource(typeof(WorkspaceDataHandler))]
    public string? WorkspaceId { get; set; }
}