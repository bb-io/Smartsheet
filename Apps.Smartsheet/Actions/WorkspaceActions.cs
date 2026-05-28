using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Models.Entities.Workspace;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Response.Workspace;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Actions;

[ActionList("Workspaces")]
public class WorkspaceActions(InvocationContext context) : SmartsheetInvocable(context)
{
    [Action("Search workspaces", Description = "Search workspaces that the user has access to")]
    public async Task<SearchWorkspacesResponse> SearchWorkspaces()
    {
        var request = new SmartsheetRequest("workspaces");
        var response = await Client.PaginateToken<WorkspaceEntity>(request)
            .Select(x => new WorkspaceResponse(x))
            .ToArrayAsync();

        return new(response);
    }
    
    [Action("Get workspace", Description = "Get a specific workspace")]
    public async Task<WorkspaceResponse> GetWorkspace([ActionParameter] WorkspaceIdentifier workspaceIdentifier)
    {
        var request = new SmartsheetRequest($"workspaces/{workspaceIdentifier.WorkspaceId}/metadata");
        var response = await Client.ExecuteWithErrorHandling<WorkspaceEntity>(request);

        return new(response);
    }
}