using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.User;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class UserDataHandler(InvocationContext context) : SmartsheetInvocable(context), IAsyncDataSourceItemHandler
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/users/list-users
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new SmartsheetRequest("users");
        return await Client.PaginateOffset<UserEntity>(request, timesToPaginate: 2)
            .WhereContains(x => x.Name, context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.ToString()))
            .ToListAsync(cancellationToken);
    }
}