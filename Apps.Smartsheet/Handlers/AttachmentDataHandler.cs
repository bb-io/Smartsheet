using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Attachment;
using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class AttachmentDataHandler : SmartsheetInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _sheetId;
    
    public AttachmentDataHandler(
        InvocationContext invocationContext,
        [ActionParameter] SheetIdentifier sheetIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(sheetIdentifier.SheetId))
            throw new PluginMisconfigurationException("Please specify a sheet ID first");

        _sheetId = sheetIdentifier.SheetId;
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/attachments/attachments-listonsheet
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new SmartsheetRequest($"sheets/{_sheetId}/attachments");
        return await Client.PaginateOffset<AttachmentEntity>(request)
            .WhereContains(x => x.Name, context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.Name))
            .ToListAsync(cancellationToken);
;    }
}