using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Discussion;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Smartsheet.Handlers;

public class DiscussionDataHandler : SmartsheetInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _sheetId;
    private readonly string? _rowId;
    
    public DiscussionDataHandler(
        InvocationContext context,
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalRowIdentifier rowIdentifier) : base(context)
    {
        if (string.IsNullOrEmpty(sheetIdentifier.SheetId))
            throw new PluginMisconfigurationException("Please specify a sheet ID first");
        
        _sheetId = sheetIdentifier.SheetId;
        _rowId = rowIdentifier.RowId;
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/discussions-list
    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/row-discussions-list
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        string endpoint = string.IsNullOrWhiteSpace(_rowId)
            ? $"sheets/{_sheetId}/discussions"
            : $"sheets/{_sheetId}/rows/{_rowId}/discussions";

        var request = new SmartsheetRequest(endpoint).AddQueryParameter("include", "comments");
        return await Client.PaginateOffset<DiscussionEntity>(request)
            .WhereContains(x => x.Title, context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.ToString()))
            .ToListAsync(cancellationToken);
    }
}