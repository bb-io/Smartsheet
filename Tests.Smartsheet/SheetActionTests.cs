using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Sheet;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class SheetActionTests : TestBase
{
    [TestMethod]
    public async Task SearchSheets_ReturnsSheets()
    {
        // Arrange
        var actions = new SheetActions(InvocationContext);
        var searchInput = new SearchSheetsRequest
        {
            
        };

        // Act
        var result = await actions.SearchSheets(searchInput);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetSheet_ReturnsSheet()
    {
        // Arrange
        var actions = new SheetActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "4709706974056324" };
        var workspaceRequest = new OptionalWorkspaceIdentifier();

        // Act
        var result = await actions.GetSheet(sheetRequest, workspaceRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreateSheetInWorkspace_ReturnsCreatedSheet()
    {
        // Arrange
        var actions = new SheetActions(InvocationContext);
        var workspaceRequest = new WorkspaceIdentifier { WorkspaceId = "3461696967731076" };
        var createRequest = new CreateSheetRequest { Name = "test from tests4" };
        var folderRequest = new OptionalFolderIdentifier { FolderId = "3836504997947268" };

        // Act
        var result = await actions.CreateSheet(workspaceRequest, createRequest, folderRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateSheet_ReturnsUpdatedSheet()
    {
        // Arrange
        var actions = new SheetActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "133455458291588" };
        var updateRequest = new UpdateSheetRequest { Name = "test123 updated" };
        var workspaceRequest = new OptionalWorkspaceIdentifier();

        // Act
        var result = await actions.UpdateSheet(sheetRequest, workspaceRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task SearchWithinAllSheets_ReturnsSearchResults()
    {
        // Arrange
        var actions = new SheetActions(InvocationContext);
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
    
    [TestMethod]
    public async Task SearchWithinSheet_ReturnsSearchResults()
    {
        // Arrange
        var actions = new SheetActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var workspaceRequest = new OptionalWorkspaceIdentifier();
        var searchRequest = new SearchWithinSheetsRequest
        {
            TextToSearch = "value"
        };

        // Act
        var result = await actions.SearchWithinSheet(sheetRequest, workspaceRequest, searchRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}