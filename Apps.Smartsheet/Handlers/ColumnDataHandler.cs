using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Column;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class ColumnDataHandler : SmartsheetInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _sheetId;
    
    public ColumnDataHandler(
        InvocationContext context, 
        [ActionParameter] SheetIdentifier sheetIdentifier) : base(context)
    {
        if (string.IsNullOrEmpty(sheetIdentifier.SheetId))
            throw new PluginMisconfigurationException("Please specify a sheet ID first");

        _sheetId = sheetIdentifier.SheetId;
    }
    
    // https://developers.smartsheet.com/api/smartsheet/openapi/columns/columns-listonsheet
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new SmartsheetRequest($"sheets/{_sheetId}/columns");
        return await Client.PaginateToken<ColumnEntity>(request, timesToPaginate: 2)
            .WhereContains(x => x.Title, context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.Title))
            .ToListAsync(ct);
    }
}