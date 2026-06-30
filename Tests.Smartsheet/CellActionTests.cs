using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Cell;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class CellActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UpdateCell_ReturnsUpdatedCell(InvocationContext context)
    {
        // Arrange
        var actions = new CellActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new RowIdentifier { RowId = "7026673031511940" };
        var columnRequest = new ColumnIdentifier { ColumnId = "8680904674545540" };
        var updateRequest = new UpdateCellRequest { NewValue = "[test2, test]" };

        // Act
        var result = await actions.UpdateCell(sheetRequest, rowRequest, columnRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod, TargetConnections(ConnectionTypes.ApiKey)]
    public async Task GetCell_ReturnsCell(InvocationContext context)
    {
        // Arrange
        var actions = new CellActions(context);
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