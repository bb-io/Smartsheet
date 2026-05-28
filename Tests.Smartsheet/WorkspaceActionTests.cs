using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class WorkspaceActionTests : TestBase
{
    [TestMethod]
    public async Task SearchWorkspaces_ReturnsWorkspaces()
    {
        // Arrange
        var actions = new WorkspaceActions(InvocationContext);

        // Act
        var result = await actions.SearchWorkspaces();

        // Assert
        PrintJsonResult(result);
        Assert.IsNotEmpty(result.Workspaces);
    }

    [TestMethod]
    public async Task GetWorkspace_ReturnsWorkspace()
    {
        // Arrange
        var actions = new WorkspaceActions(InvocationContext);
        var workspaceId = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };

        // Act
        var result = await actions.GetWorkspace(workspaceId);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}