using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class FolderActionTests : TestBase
{
    [TestMethod]
    public async Task GetFolder_ReturnsFolder()
    {
        // Arrange
        var actions = new FolderActions(InvocationContext);
        var folderRequest = new FolderIdentifier { FolderId = "3836504997947268" };
        var workspaceRequest = new OptionalWorkspaceIdentifier();
        
        // Act
        var result = await actions.GetFolder(folderRequest, workspaceRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}