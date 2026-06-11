using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Comment;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class CommentActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetComment_ReturnsComment(InvocationContext context)
    {
        // Arrange
        var actions = new CommentActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var commentRequest = new CommentIdentifier { CommentId = "6621919295606660" };

        // Act
        var result = await actions.GetComment(sheetRequest, commentRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task CreateComment_ReturnsCreatedComment(InvocationContext context)
    {
        // Arrange
        var actions = new CommentActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var discussionRequest = new DiscussionIdentifier { DiscussionId = "2772422782128004" };
        var createRequest = new CreateCommentRequest { CommentText = "test comment" };

        // Act
        var result = await actions.CreateComment(sheetRequest, discussionRequest, createRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UpdateComment_ReturnsUpdatedComment(InvocationContext context)
    {
        // Arrange
        var actions = new CommentActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var commentRequest = new CommentIdentifier { CommentId = "6621919295606660" };
        var updateRequest = new UpdateCommentRequest { CommentText = "this is updated" };

        // Act
        var result = await actions.UpdateComment(sheetRequest, commentRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DeleteComment_IsSuccess(InvocationContext context)
    {
        // Arrange
        var actions = new CommentActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var commentRequest = new CommentIdentifier { CommentId = "6621919295606660" };

        // Act
        await actions.DeleteComment(sheetRequest, commentRequest);

        // Assert
        var ex = await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await actions.GetComment(sheetRequest, commentRequest));
        
        Assert.Contains("Not Found", ex.Message);
    }
}