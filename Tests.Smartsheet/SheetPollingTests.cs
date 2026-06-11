using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Polling;
using Apps.Smartsheet.Polling.Models.Memory;
using Apps.Smartsheet.Polling.Models.Request.Sheet;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class SheetPollingTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task OnSheetsCreated_IsSuccess(InvocationContext context)
    {
        // Arrange
        var polling = new SheetPollingList(context);
        var dateMemory = new DateMemory { LastInteraction = DateTime.UtcNow - TimeSpan.FromHours(2) };
        var pollingRequest = new PollingEventRequest<DateMemory> { Memory = dateMemory };
        var createInput = new SheetsCreatedRequest { NameContains = "" };

        // Act
        var result = await polling.OnSheetsCreated(pollingRequest, createInput);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}