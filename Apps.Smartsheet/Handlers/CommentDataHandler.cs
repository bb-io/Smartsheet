using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Discussion;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Handlers;

public class CommentDataHandler : SmartsheetInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _sheetId;
    private readonly string _discussionId;
    
    public CommentDataHandler(
        InvocationContext invocationContext,
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalDiscussionIdentifier discussionIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(sheetIdentifier.SheetId))
            throw new PluginMisconfigurationException("Please specify a sheet ID first");
        
        if (string.IsNullOrEmpty(discussionIdentifier.DiscussionId))
            throw new PluginMisconfigurationException("Please specify a discussion ID first");

        _sheetId = sheetIdentifier.SheetId;
        _discussionId = discussionIdentifier.DiscussionId;
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/discussion-get
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new SmartsheetRequest($"sheets/{_sheetId}/discussions/{_discussionId}");
        var response = await Client.ExecuteWithErrorHandling<DiscussionEntity>(request);

        return response.Comments
            .WhereContains(x => x.Text, context.SearchString)
            .Select(x => new DataSourceItem(x.Id, x.Text.Limit(60)))
            .ToList();
    }
}