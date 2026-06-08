using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Contacts;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class ContactDataHandler(InvocationContext context) : SmartsheetInvocable(context), IAsyncDataSourceItemHandler
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/contacts/list-contacts
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new SmartsheetRequest("contacts");
        return await Client.PaginateOffset<ContactEntity>(request)
            .WhereContains(x => x.ToString(), context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.ToString()))
            .ToArrayAsync(cancellationToken);
    }
}