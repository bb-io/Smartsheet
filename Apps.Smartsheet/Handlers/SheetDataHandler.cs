using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Utility.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class SheetDataHandler(InvocationContext context) : SmartsheetInvocable(context), IAsyncDataSourceItemHandler
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/list-sheets
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new SmartsheetRequest("sheets");
        var response = await Client.ExecuteWithErrorHandling<OffsetPaginationResponse<SheetEntity>>(request);
            
        return response.Data
            .WhereContains(x => x.Name, context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.Name))
            .ToList();
    }
}