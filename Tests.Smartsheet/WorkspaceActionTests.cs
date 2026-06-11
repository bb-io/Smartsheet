using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class WorkspaceActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchWorkspaces_ReturnsWorkspaces(InvocationContext context)
    {
        // Arrange
        var actions = new WorkspaceActions(context);

        // Act
        var result = await actions.SearchWorkspaces();

        // Assert
        PrintJsonResult(result);
        Assert.IsNotEmpty(result.Workspaces);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetWorkspace_ReturnsWorkspace(InvocationContext context)
    {
        // Arrange
        var actions = new WorkspaceActions(context);
        var workspaceId = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };

        // Act
        var result = await actions.GetWorkspace(workspaceId);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}