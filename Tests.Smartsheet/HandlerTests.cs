using Apps.Smartsheet.Handlers;
using Apps.Smartsheet.Handlers.FileFolder;
using Apps.Smartsheet.Models.Identifiers;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class HandlerTests : TestBase
{
    [TestMethod]
    public async Task UserDataHandler_ReturnsUsers()
    {
        // Arrange
        var handler = new UserDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task WorkspaceDataHandler_ReturnsWorkspaces()
    {
        // Arrange
        var handler = new WorkspaceDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task SheetPickerDataHandler_ReturnsSheets()
    {
        // Arrange
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var handler = new SheetPickerDataHandler(InvocationContext, workspaceRequest);

        // Act
        var result = await handler.GetFolderContentAsync(new() { FolderId = "" }, CancellationToken.None);

        // Assert
        PrintFileFolderPickerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task FolderPickerDataHandler_ReturnsFolders()
    {
        // Arrange
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var handler = new FolderPickerDataHandler(InvocationContext, workspaceRequest);

        // Act
        var result = await handler.GetFolderContentAsync(new() { FolderId = "8172027522639748" }, CancellationToken.None);

        // Assert
        PrintFileFolderPickerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task ColumnDataHandler_ReturnsColumns()
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var handler = new ColumnDataHandler(InvocationContext, sheetIdentifier);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "5" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task RowDataHandler_ReturnsColumns()
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var handler = new RowDataHandler(InvocationContext, sheetIdentifier);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task ContactDataHandler_ReturnsContacts()
    {
        // Arrange
        var handler = new ContactDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
}
