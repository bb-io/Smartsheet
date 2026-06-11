using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Discussion;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class DiscussionActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchDiscussions_ReturnsDiscussions(InvocationContext context)
    {
        // Arrange
        var actions = new DiscussionActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "4568057153257348" };

        // Act
        var result = await actions.SearchDiscussions(sheetRequest, rowRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetDiscussion_ReturnsDiscussion(InvocationContext context)
    {
        // Arrange
        var actions = new DiscussionActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var discussionRequest = new DiscussionIdentifier { DiscussionId = "8273197239144324" };

        // Act
        var result = await actions.GetDiscussion(sheetRequest, discussionRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task CreateDiscussion_ReturnsCreatedDiscussion(InvocationContext context)
    {
        // Arrange
        var actions = new DiscussionActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "4568057153257348" };
        var createRequest = new CreateDiscussionRequest { DiscussionText = "test from tests2" };

        // Act
        var result = await actions.CreateDiscussion(sheetRequest, rowRequest, createRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DeleteDiscussion_IsSuccess(InvocationContext context)
    {
        // Arrange
        var actions = new DiscussionActions(context);
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