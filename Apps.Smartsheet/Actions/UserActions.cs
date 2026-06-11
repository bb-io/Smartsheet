using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Models.Entities.User;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Response.User;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Actions;

[ActionList("Users")]
public class UserActions(InvocationContext invocationContext) : SmartsheetInvocable(invocationContext)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/users/list-users
    [Action("Search users", Description = "Search users in the organization account")]
    public async Task<SearchUsersResponse> SearchUsers()
    {
        var request = new SmartsheetRequest("users");
        var response = await Client.PaginateOffset<UserEntity>(request)
            .Select(entity => new UserResponse(entity))
            .ToArrayAsync();

        return new SearchUsersResponse(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/users/get-user
    [Action("Get user", Description = "Get a specific user")]
    public async Task<UserResponse> GetUser([ActionParameter] UserIdentifier userIdentifier)
    {
        var request = new SmartsheetRequest($"users/{userIdentifier.UserId}");
        var response = await Client.ExecuteWithErrorHandling<UserEntity>(request);
        return new(response);
    }
}