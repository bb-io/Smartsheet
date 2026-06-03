using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
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

        // Act
        var result = await actions.GetSheet(sheetRequest);

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
        var createRequest = new CreateSheetInWorkspaceRequest { Name = "test from tests4" };

        // Act
        var result = await actions.CreateSheetInWorkspace(workspaceRequest, createRequest);

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

        // Act
        var result = await actions.UpdateSheet(sheetRequest, updateRequest);

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
}