using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
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
}
