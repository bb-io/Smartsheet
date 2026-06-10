using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Folder;
using Blackbird.Applications.Sdk.Common.Exceptions;
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
        var folderRequest = new FolderIdentifier
        {
            FolderId = "5003250580645764",
            WorkspaceId = ""
        };
        
        // Act
        var result = await actions.GetFolder(folderRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetFolderPath_ReturnsPathString()
    {
        // Arrange
        var actions = new FolderActions(InvocationContext);
        var folderRequest = new FolderIdentifier
        {
            FolderId = "5003250580645764", 
            WorkspaceId = ""
        };
        
        // Act
        var result = await actions.GetFolderPath(folderRequest);

        // Assert
        Console.WriteLine(result.Path);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreateFolder_ReturnsCreatedFolder()
    {
        // Arrange
        var actions = new FolderActions(InvocationContext);
        var folderRequest = new OptionalFolderIdentifier { FolderId = "5003250580645764" };
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var createRequest = new CreateFolderRequest { FolderName = "222test new" };

        // Act
        var result = await actions.CreateFolder(workspaceRequest, folderRequest, createRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateFolder_ReturnsUpdatedFolder()
    {
        // Arrange
        var actions = new FolderActions(InvocationContext);
        var folderRequest = new FolderIdentifier
        {
            FolderId = "7686058952419204",
            WorkspaceId = "3461696967731076"
        };
        var updateRequest = new UpdateFolderRequest { FolderName = "123test new" };

        // Act
        var result = await actions.UpdateFolder(folderRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DeleteFolder_IsSuccess()
    {
        // Arrange
        var actions = new FolderActions(InvocationContext);
        var folderRequest = new FolderIdentifier
        {
            FolderId = "7686058952419204",
            WorkspaceId = ""
        };

        // Act
        await actions.DeleteFolder(folderRequest);

        // Assert
        var ex = await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await actions.GetFolder(folderRequest));
        
        Assert.Contains("Not Found", ex.Message);
    }
}