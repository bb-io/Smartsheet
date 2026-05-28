using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Workspace;
using Apps.Smartsheet.Models.Utility.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class WorkspaceDataHandler(InvocationContext context) : SmartsheetInvocable(context), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new SmartsheetRequest("workspaces");
        var response = await Client.ExecuteWithErrorHandling<TokenPaginationResponse<WorkspaceEntity>>(request);

        return response.Data
            .Where(x => x.Name.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Id, x.Name))
            .ToList();
    }
}