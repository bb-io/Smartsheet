using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class UserActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchUsers_ReturnsUsers(InvocationContext context)
    {
        // Arrange
        var actions = new UserActions(context);

        // Act
        var result = await actions.SearchUsers();

        // Assert
        PrintJsonResult(result);
        Assert.IsNotEmpty(result.Users);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetUser_ReturnsUser(InvocationContext context)
    {
        // Arrange
        var actions = new UserActions(context);
        var userId = new UserIdentifier { UserId = "4905085051398020" };

        // Act
        var result = await actions.GetUser(userId);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}