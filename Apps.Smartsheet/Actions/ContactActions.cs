using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Extensions;
using Apps.Smartsheet.Models.Entities.Contacts;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Contacts;
using Apps.Smartsheet.Models.Response.Contacts;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Actions;

[ActionList("Contacts")]
public class ContactActions(InvocationContext context) : SmartsheetInvocable(context)
{
    // https://developers.smartsheet.com/api/smartsheet/openapi/contacts/list-contacts
    [Action("Search contacts", Description = "Search through the user's contacts")]
    public async Task<SearchContactsResponse> SearchContacts([ActionParameter] SearchContactsRequest searchInput)
    {
        var request = new SmartsheetRequest("contacts");
        var response = await Client.PaginateOffset<ContactEntity>(request)
            .WhereContains(x => x.Name, searchInput.NameContains)
            .WhereContains(x => x.Email, searchInput.EmailContains)
            .Select(x => new ContactResponse(x))
            .ToArrayAsync();

        return new(response);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/contacts/get-contact
    [Action("Get contact", Description = "Get a specific contact")]
    public async Task<ContactResponse> GetContact([ActionParameter] ContactIdentifier contactInput)
    {
        var request = new SmartsheetRequest($"contacts/{contactInput.ContactId}");
        var response = await Client.ExecuteWithErrorHandling<ContactEntity>(request);

        return new(response);
    }
}