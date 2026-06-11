using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Helper.Error;
using Apps.Smartsheet.Models.Entities.Discussion;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Apps.Smartsheet.Models.Request.Discussion;
using Apps.Smartsheet.Models.Response.Discussion;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Actions;

[ActionList("Discussions")]
public class DiscussionActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/discussions-list
    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/row-discussions-list
    [Action("Search discussions", Description = "Search through all discussions for a specified sheet, optionally filtered by row")]
    public async Task<SearchDiscussionsResponse> SearchDiscussions(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalRowIdentifier rowIdentifier)
    {
        string endpoint = string.IsNullOrWhiteSpace(rowIdentifier.RowId)
            ? $"sheets/{sheetIdentifier.SheetId}/discussions"
            : $"sheets/{sheetIdentifier.SheetId}/rows/{rowIdentifier.RowId}/discussions";

        var request = new SmartsheetRequest(endpoint).AddQueryParameter("include", "comments");
        var response = await Client.PaginateOffset<DiscussionEntity>(request)
            .Select(x => new DiscussionResponse(x))
            .ToArrayAsync();

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/discussion-get
    [Action("Get discussion", Description = "Get a specific discussion")]
    public async Task<DiscussionResponse> GetDiscussion(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] DiscussionIdentifier discussionIdentifier)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/discussions/{discussionIdentifier.DiscussionId}";
        var request = new SmartsheetRequest(endpoint);
        var response = await Client.ExecuteWithErrorHandling<DiscussionEntity>(request);

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/discussions-create
    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/row-discussions-create
    [Action("Create discussion", Description = "Create a new discussion on a sheet or a row")]
    public async Task<DiscussionResponse> CreateDiscussion(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] OptionalRowIdentifier rowIdentifier,
        [ActionParameter] CreateDiscussionRequest createInput)
    {
        string endpoint = string.IsNullOrWhiteSpace(rowIdentifier.RowId)
            ? $"sheets/{sheetIdentifier.SheetId}/discussions"
            : $"sheets/{sheetIdentifier.SheetId}/rows/{rowIdentifier.RowId}/discussions";

        var request = new SmartsheetRequest(endpoint, Method.Post)
            .WithJsonBody(new
            {
                comment = new { text = createInput.DiscussionText }
            });
        var response = await Client.ExecuteWithErrorHandling<Result<DiscussionEntity>>(request);

        return new(response.Value);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/discussions/discussion-delete
    [Action("Delete discussion", Description = "Delete a specific discussion")]
    public async Task DeleteDiscussion(
        [ActionParameter] SheetIdentifier sheetIdentifier,
        [ActionParameter] DiscussionIdentifier discussionIdentifier)
    {
        string endpoint = $"sheets/{sheetIdentifier.SheetId}/discussions/{discussionIdentifier.DiscussionId}";
        var request = new SmartsheetRequest(endpoint, Method.Delete);
        var response = await Client.ExecuteWithErrorHandling<Result>(request);
        
        if (!response.IsSuccessfulResponse)
            throw new PluginApplicationException(ErrorMessageHelper.GenerateFailedToDeleteMessage("discussion"));
    }
}