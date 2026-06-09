using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Attachment;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class AttachmentActionTests : TestBase
{
    [TestMethod]
    public async Task UploadAttachment_ReturnsUploadedAttachmentMeta()
    {
        // Arrange
        var actions = new AttachmentActions(InvocationContext, FileManager);
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

    [TestMethod]
    public async Task GetAttachment_ReturnsAttachment()
    {
        // Arrange
        var actions = new AttachmentActions(InvocationContext, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var attachmentRequest = new AttachmentIdentifier { AttachmentId = "6724129434013572" };

        // Act
        var result = await actions.GetAttachment(sheetRequest, attachmentRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DownloadAttachment_IsSuccess()
    {
        // Arrange
        var actions = new AttachmentActions(InvocationContext, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var attachmentRequest = new AttachmentIdentifier { AttachmentId = "6724129434013572" };

        // Act
        var result = await actions.DownloadAttachment(sheetRequest, attachmentRequest);

        // Assert
        Console.WriteLine(result.File.Name);
        Assert.IsNotNull(result.File);
    }

    [TestMethod]
    public async Task SearchAttachments_ReturnsAttachments()
    {
        // Arrange
        var actions = new AttachmentActions(InvocationContext, FileManager);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new OptionalRowIdentifier { RowId = "" };
        var discussionRequest = new OptionalDiscussionIdentifier { DiscussionId = "2772422782128004" };

        // Act
        var result = await actions.SearchAttachments(sheetRequest, rowRequest, discussionRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DeleteAttachment_IsSuccess()
    {
        // Arrange
        var actions = new AttachmentActions(InvocationContext, FileManager);
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