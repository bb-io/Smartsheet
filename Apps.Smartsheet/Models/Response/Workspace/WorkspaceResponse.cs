using Apps.Smartsheet.Models.Entities.Workspace;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Workspace;

public record WorkspaceResponse
{
    public WorkspaceResponse(WorkspaceEntity workspace)
    {
        Id = workspace.Id;
        Name = workspace.Name;
        AccessLevel = workspace.AccessLevel;
        Permalink = workspace.Permalink;
    }

    [Display("Workspace ID")]
    public string Id { get; set; }

    [Display("Workspace name")]
    public string Name { get; set; }

    [Display("Workspace access level")]
    public string AccessLevel { get; set; }

    [Display("Workspace URL")]
    public string Permalink { get; set; }
}