using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Cell;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class CellActionTests : TestBase
{
    [TestMethod]
    public async Task UpdateCell_ReturnsUpdatedCell()
    {
        // Arrange
        var actions = new CellActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new RowIdentifier { RowId = "7026673031511940" };
        var columnRequest = new ColumnIdentifier { ColumnId = "8680904674545540" };
        var updateRequest = new UpdateCellRequest { NewValue = "[test3, test2]" };

        // Act
        var result = await actions.UpdateCell(sheetRequest, rowRequest, columnRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task GetCell_ReturnsCell()
    {
        // Arrange
        var actions = new CellActions(InvocationContext);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new RowIdentifier { RowId = "4568057153257348" };
        var columnRequest = new ColumnIdentifier { ColumnId = "750975731404676" };

        // Act
        var result = await actions.GetCell(sheetRequest, rowRequest, columnRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}