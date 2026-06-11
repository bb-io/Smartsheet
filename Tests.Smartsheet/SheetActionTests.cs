using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Sheet;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class SheetActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchSheets_ReturnsSheets(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var searchInput = new SearchSheetsRequest
        {
            
        };

        // Act
        var result = await actions.SearchSheets(searchInput);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetSheet_ReturnsSheet(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var sheetRequest = new SheetIdentifier
        {
            SheetId = "4709706974056324",
            WorkspaceId = ""
        };

        // Act
        var result = await actions.GetSheet(sheetRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task CreateSheetInWorkspace_ReturnsCreatedSheet(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var createRequest = new CreateSheetRequest { Name = "test from tests4" };
        var folderRequest = new OptionalFolderIdentifier { FolderId = "3836504997947268" };

        // Act
        var result = await actions.CreateSheet(workspaceRequest, createRequest, folderRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UpdateSheet_ReturnsUpdatedSheet(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var sheetRequest = new SheetIdentifier
        {
            SheetId = "133455458291588",
            WorkspaceId = ""
        };
        var updateRequest = new UpdateSheetRequest { Name = "test123 updated" };

        // Act
        var result = await actions.UpdateSheet(sheetRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchWithinAllSheets_ReturnsSearchResults(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var searchRequest = new SearchWithinSheetsRequest
        {
            TextToSearch = "value"
        };

        // Act
        var result = await actions.SearchWithinAllSheets(searchRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchWithinSheet_ReturnsSearchResults(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var sheetRequest = new SheetIdentifier
        {
            SheetId = "3188607262084996",
            WorkspaceId = ""
        };
        var searchRequest = new SearchWithinSheetsRequest
        {
            TextToSearch = "value"
        };

        // Act
        var result = await actions.SearchWithinSheet(sheetRequest, searchRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DownloadSheet_IsSuccess(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var sheetRequest = new SheetIdentifier
        {
            SheetId = "3188607262084996",
            WorkspaceId = ""
        };
        var fileFormatRequest = new FileFormatIdentifier { FileFormat = SheetFileFormats.Csv };
        var downloadRequest = new DownloadSheetRequest { FileName = "absolute cinema2" };

        // Act
        var result = await actions.DownloadSheet(sheetRequest, fileFormatRequest, downloadRequest);

        // Assert
        TestContext?.WriteLine(result.File.Name);
        Assert.IsNotNull(result.File);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UploadSheet_ReturnsUploadedSheet(InvocationContext context)
    {
        // Arrange
        var actions = new SheetActions(context, FileManager);
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var folderRequest = new OptionalFolderIdentifier { FolderId = "8172027522639748" };
        var uploadRequest = new UploadSheetRequest
        {
            File = new FileReference { Name = "Sheet.csv" },
            OverwrittenSheetName = "test"
        };

        // Act
        var result = await actions.UploadSheet(workspaceRequest, folderRequest, uploadRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}