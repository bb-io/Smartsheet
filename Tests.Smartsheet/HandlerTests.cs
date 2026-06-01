using Apps.Smartsheet.Handlers;
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
    public async Task WorkspaceDataHandler_ReturnsUsers()
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
    public async Task SheetDataHandler_ReturnsUsers()
    {
        // Arrange
        var handler = new SheetDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new() { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
}
