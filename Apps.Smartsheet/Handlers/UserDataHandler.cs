using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.User;
using Apps.Smartsheet.Models.Utility.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class UserDataHandler(InvocationContext context) : SmartsheetInvocable(context), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new SmartsheetRequest("users");
        var response = await Client.ExecuteWithErrorHandling<OffsetPaginationResponse<UserEntity>>(request);

        return response.Data
            .Where(x => x.Name.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Id, x.ToString()))
            .ToList();
    }
}