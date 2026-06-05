using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Column;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class ColumnActionTests : TestBase
{
    [TestMethod]
    public async Task SearchColumns_ReturnsColumns()
    {
        // Arrange
        var actions = new ColumnActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var searchRequest = new SearchColumnsRequest { ColumnTitleContains = "" };

        // Act
        var result = await actions.SearchColumns(sheetRequest, searchRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task GetColumn_ReturnsColumn()
    {
        // Arrange
        var actions = new ColumnActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var columnRequest = new ColumnIdentifier { ColumnId = "3565725498511236" };

        // Act
        var result = await actions.GetColumn(sheetRequest, columnRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task AddColumn_ReturnsCreatedColumn()
    {
        // Arrange
        var actions = new ColumnActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var addRequest = new AddColumnRequest
        {
            Title = "new test",
            Type = "CHECKBOX",
            Description = "from tests!",
            Index = 2,
            Width = 100
        };

        // Act
        var result = await actions.AddColumn(sheetRequest, addRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task UpdateColumn_ReturnsUpdateColumn()
    {
        // Arrange
        var actions = new ColumnActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var columnRequest = new ColumnIdentifier { ColumnId = "8064937711931268" };
        var addRequest = new UpdateColumnRequest
        {
            Title = "new test1",
            Type = "TEXT_NUMBER",
            Description = "updated from tests!",
            Index = 4,
            Width = 50,
            Formula = "=1+1",
            IsLocked = true
        };

        // Act
        var result = await actions.UpdateColumn(sheetRequest, columnRequest, addRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DeleteColumn_IsSuccess()
    {
        // Arrange
        var actions = new ColumnActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var columnRequest = new ColumnIdentifier { ColumnId = "8064937711931268" };

        // Act
        await actions.DeleteColumn(sheetRequest, columnRequest);

        // Assert
        var ex = await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await actions.GetColumn(sheetRequest, columnRequest));
        
        Assert.Contains("Not Found", ex.Message);
    }
}