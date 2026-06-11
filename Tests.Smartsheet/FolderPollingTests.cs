using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Polling;
using Apps.Smartsheet.Polling.Models.Memory;
using Apps.Smartsheet.Polling.Models.Request.Folder;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class FolderPollingTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task OnFoldersCreated_IsSuccess(InvocationContext context)
    {
        // Arrange
        var polling = new FolderPollingList(context);
        var dateMemory = new DateMemory { LastInteraction = DateTime.UtcNow - TimeSpan.FromHours(2) };
        var pollingRequest = new PollingEventRequest<DateMemory> { Memory = dateMemory };
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var folderRequest = new OptionalFolderIdentifier { FolderId = "5003250580645764" };
        var createInput = new FoldersCreatedRequest { NameContains = "89" };

        // Act
        var result = await polling.OnFoldersCreated(pollingRequest, workspaceRequest, folderRequest, createInput);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}