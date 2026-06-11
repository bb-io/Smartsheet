using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Contacts;
using Blackbird.Applications.Sdk.Common.Invocation;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class ContactActionTests : TestBaseMultipleConnections
{
    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task SearchContacts_ReturnsContacts(InvocationContext context)
    {
        // Arrange
        var actions = new ContactActions(context);
        var request = new SearchContactsRequest
        {
            EmailContains = "email.com"
        };

        // Act
        var result = await actions.SearchContacts(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod, TargetConnections(ConnectionTypes.OAuth, ConnectionTypes.ApiKey)]
    public async Task GetContact_ReturnsContact(InvocationContext context)
    {
        // Arrange
        var actions = new ContactActions(context);
        var contactRequest = new ContactIdentifier { ContactId = "AAAAATYU54QAGI2L7BTnhA" };

        // Act
        var result = await actions.GetContact(contactRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}