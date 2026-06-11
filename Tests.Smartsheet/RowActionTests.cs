using System.Globalization;
using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Row;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class RowActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetRow_ReturnsRow(InvocationContext context)
    {
        // Arrange
        var actions = new RowActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new RowIdentifier { RowId = "7026673031511940" };

        // Act
        var result = await actions.GetRow(sheetRequest, rowRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task AddRow_ReturnsCreatedRow(InvocationContext context)
    {
        // Arrange
        var actions = new RowActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var currentDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        var addRequest = new AddRowRequest
        {
            ColumnIds =    ["5817525312196484", "750975731404676", "7335646829252484"],
            ColumnValues = ["test"            , currentDate      , "true"            ],
            AppendToTop = true
        };

        // Act
        var result = await actions.AddRow(sheetRequest, addRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task UpdateRow_ReturnsUpdatedRow(InvocationContext context)
    {
        // Arrange
        var actions = new RowActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new RowIdentifier { RowId = "7026673031511940" };
        var currentDateString = (DateTime.UtcNow - TimeSpan.FromDays(3)).ToString(CultureInfo.InvariantCulture);
        var updateRequest = new UpdateRowRequest
        {
            ColumnIds =    ["5817525312196484", "750975731404676", "7335646829252484", "8680904674545540"],
            ColumnValues = ["test1"           , currentDateString, "true"            , "[test3, test2, test]" ]
        };

        // Act
        var result = await actions.UpdateRow(sheetRequest, rowRequest, updateRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task DeleteRow_IsSuccess(InvocationContext context)
    {
        // Arrange
        var actions = new RowActions(context);
        var sheetRequest = new SheetIdentifier { SheetId = "3188607262084996" };
        var rowRequest = new RowIdentifier { RowId = "5698729171419012" };

        // Act
        await actions.DeleteRow(sheetRequest, rowRequest);

        // Assert
        var ex = await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await actions.GetRow(sheetRequest, rowRequest));
        
        Assert.Contains("Not Found", ex.Message);
    }
}