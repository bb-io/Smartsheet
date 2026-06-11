using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Sheet;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class RowDataHandler : SmartsheetInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _sheetId;
    
    public RowDataHandler(
        InvocationContext invocationContext, 
        [ActionParameter] SheetIdentifier sheetIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(sheetIdentifier.SheetId))
            throw new PluginMisconfigurationException("Please specify a sheet ID first");

        _sheetId = sheetIdentifier.SheetId;
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/sheets/getsheet
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new SmartsheetRequest($"sheets/{_sheetId}");
        var response = await Client.ExecuteWithErrorHandling<SheetEntity>(request);
        var rows = response.Rows;

        var items = rows.Select(x => new DataSourceItem(x.Id, x.ToString().Limit(60)));
        return items.WhereContains(x => x.DisplayName, context.SearchString).ToList();
    }
}