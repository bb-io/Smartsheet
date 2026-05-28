using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class UserActionTests : TestBase
{
    [TestMethod]
    public async Task SearchUsers_ReturnsUsers()
    {
        // Arrange
        var actions = new UserActions(InvocationContext);

        // Act
        var result = await actions.SearchUsers();

        // Assert
        PrintJsonResult(result);
        Assert.IsNotEmpty(result.Users);
    }

    [TestMethod]
    public async Task GetUser_ReturnsUser()
    {
        // Arrange
        var actions = new UserActions(InvocationContext);
        var userId = new UserIdentifier { UserId = "4905085051398020" };

        // Act
        var result = await actions.GetUser(userId);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}
