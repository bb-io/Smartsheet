using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Discussion;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class DiscussionActionTests : TestBase
{
    [TestMethod]
    public async Task SearchDiscussions_ReturnsDiscussions()
    {
        // Arrange
        var actions = new DiscussionActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "4568057153257348" };

        // Act
        var result = await actions.SearchDiscussions(sheetRequest, rowRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetDiscussion_ReturnsDiscussion()
    {
        // Arrange
        var actions = new DiscussionActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var discussionRequest = new DiscussionIdentifier { DiscussionId = "8273197239144324" };

        // Act
        var result = await actions.GetDiscussion(sheetRequest, discussionRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreateDiscussion_ReturnsCreatedDiscussion()
    {
        // Arrange
        var actions = new DiscussionActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "4568057153257348" };
        var createRequest = new CreateDiscussionRequest { DiscussionText = "test from tests2" };

        // Act
        var result = await actions.CreateDiscussion(sheetRequest, rowRequest, createRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DeleteDiscussion_IsSuccess()
    {
        // Arrange
        var actions = new DiscussionActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var discussionRequest = new DiscussionIdentifier { DiscussionId = "8273197239144324" };

        // Act
        await actions.DeleteDiscussion(sheetRequest, discussionRequest);

        // Assert
        var ex = await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await actions.GetDiscussion(sheetRequest, discussionRequest));
        
        Assert.Contains("Not Found", ex.Message);
    }
}