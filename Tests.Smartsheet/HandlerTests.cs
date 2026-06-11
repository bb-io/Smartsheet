using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Handlers;
using Apps.Smartsheet.Handlers.FileFolder;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class HandlerTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UserDataHandler_ReturnsUsers(InvocationContext context)
    {
        // Arrange
        var handler = new UserDataHandler(context);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task WorkspaceDataHandler_ReturnsWorkspaces(InvocationContext context)
    {
        // Arrange
        var handler = new WorkspaceDataHandler(context);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SheetPickerDataHandler_ReturnsSheets(InvocationContext context)
    {
        // Arrange
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var handler = new SheetPickerDataHandler(context, workspaceRequest);

        // Act
        var result = await handler.GetFolderContentAsync(new() { FolderId = "" }, CancellationToken.None);

        // Assert
        PrintFileFolderPickerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task FolderPickerDataHandler_ReturnsFolders(InvocationContext context)
    {
        // Arrange
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var handler = new FolderPickerDataHandler(context, workspaceRequest);

        // Act
        var result = await handler.GetFolderContentAsync(new() { FolderId = "8172027522639748" }, CancellationToken.None);

        // Assert
        PrintFileFolderPickerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task ColumnDataHandler_ReturnsColumns(InvocationContext context)
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var handler = new ColumnDataHandler(context, sheetIdentifier);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task RowDataHandler_ReturnsColumns(InvocationContext context)
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var handler = new RowDataHandler(context, sheetIdentifier);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task ContactDataHandler_ReturnsContacts(InvocationContext context)
    {
        // Arrange
        var handler = new ContactDataHandler(context);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DiscussionDataHandler_ReturnsDiscussions(InvocationContext context)
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowIdentifier = new OptionalRowIdentifier { RowId = "" };
        var handler = new DiscussionDataHandler(context, sheetIdentifier, rowIdentifier);
        
        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task CommentDataHandler_ReturnsComments(InvocationContext context)
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var discussionIdentifier = new OptionalDiscussionIdentifier { DiscussionId = "2772422782128004" };
        var handler = new CommentDataHandler(context, sheetIdentifier, discussionIdentifier);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task AttachmentDataHandler_ReturnsAttachments(InvocationContext context)
    {
        // Arrange
        var sheetIdentifier = new SheetIdentifier { SheetId = "3188607262084996" };
        var handler = new AttachmentDataHandler(context, sheetIdentifier);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
}