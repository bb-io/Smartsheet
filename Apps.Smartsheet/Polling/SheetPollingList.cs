using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Helper.Polling;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Response.Sheet;
using Apps.Smartsheet.Models.Utility.Pagination;
using Apps.Smartsheet.Polling.Models.Memory;
using Apps.Smartsheet.Polling.Models.Request.Sheet;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Smartsheet.Polling;

[PollingEventList("Sheets")]
public class SheetPollingList(InvocationContext context) : SmartsheetInvocable(context)
{
    [PollingEvent("On sheets created", Description = "Triggers when new sheets are created")]
    public async Task<PollingEventResponse<DateMemory, SearchSheetsResponse>> OnSheetsCreated(
        PollingEventRequest<DateMemory> pollingRequest,
        [PollingEventParameter] SheetsCreatedRequest createInput)
    {
        var interactionTimestamp = DateTime.UtcNow;
        
        if (pollingRequest.Memory?.LastInteraction is null)
            return PollingHelper.DontFlyBird<SearchSheetsResponse>(interactionTimestamp);

        var request = new SmartsheetRequest("sheets")
            .AddQueryParameter("modifiedSince", pollingRequest.Memory.LastInteraction.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            .AddQueryParameter("includeAll", "true");
        
        var response = await Client.ExecuteWithErrorHandling<OffsetPaginationResponse<SheetEntity>>(request);
        var filtered = response.Data
            .Where(x => x.CreatedAt > pollingRequest.Memory.LastInteraction.Value)
            .WhereContains(x => x.Name, createInput.NameContains)
            .Select(x => new SheetResponse(x))
            .ToArray();
        
        return filtered.Length == 0 
            ? PollingHelper.DontFlyBird<SearchSheetsResponse>(interactionTimestamp) 
            : PollingHelper.FlyBird<SearchSheetsResponse>(new(filtered), interactionTimestamp);
    }
}