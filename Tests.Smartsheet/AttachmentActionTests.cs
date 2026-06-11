using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Attachment;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class AttachmentActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UploadAttachment_ReturnsUploadedAttachmentMeta(InvocationContext context)
    {
        // Arrange
        var actions = new AttachmentActions(context, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "271273590456196" };
        var commentRequest = new OptionalCommentIdentifier { CommentId = "" };
        var uploadRequest = new UploadAttachmentRequest
        {
            File = new FileReference
            {
                Name = "helloworld.txt",
                ContentType = "text/plain"
            }
        };

        // Act
        var result = await actions.UploadAttachment(sheetRequest, rowRequest, commentRequest, uploadRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetAttachment_ReturnsAttachment(InvocationContext context)
    {
        // Arrange
        var actions = new AttachmentActions(context, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var attachmentRequest = new AttachmentIdentifier { AttachmentId = "4342197144424324" };

        // Act
        var result = await actions.GetAttachment(sheetRequest, attachmentRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DownloadAttachment_IsSuccess(InvocationContext context)
    {
        // Arrange
        var actions = new AttachmentActions(context, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var attachmentRequest = new AttachmentIdentifier { AttachmentId = "5297247211655044" };

        // Act
        var result = await actions.DownloadAttachment(sheetRequest, attachmentRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result.File);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchAttachments_ReturnsAttachments(InvocationContext context)
    {
        // Arrange
        var actions = new AttachmentActions(context, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "" };
        var discussionRequest = new OptionalDiscussionIdentifier { DiscussionId = "2772422782128004" };

        // Act
        var result = await actions.SearchAttachments(sheetRequest, rowRequest, discussionRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DeleteAttachment_IsSuccess(InvocationContext context)
    {
        // Arrange
        var actions = new AttachmentActions(context, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var attachmentRequest = new AttachmentIdentifier { AttachmentId = "6724129434013572" };

        // Act
        await actions.DeleteAttachment(sheetRequest, attachmentRequest);

        // Assert
        var ex = await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await actions.GetAttachment(sheetRequest, attachmentRequest));
        
        Assert.Contains("Not Found", ex.Message);
    }
}