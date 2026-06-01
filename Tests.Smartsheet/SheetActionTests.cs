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
            CreatedAfter = new DateTime(2026, 05, 28, 15, 37, 00, DateTimeKind.Utc),
            ModifiedBefore = new DateTime(2026, 05, 28, 15, 39, 00, DateTimeKind.Utc),
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
        var createRequest = new CreateSheetInWorkspaceRequest { Name = "test from tests" };

        // Act
        var result = await actions.CreateSheetInWorkspace(workspaceRequest, createRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}