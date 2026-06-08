using Apps.Smartsheet.Actions;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Request.Contacts;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class ContactActionTests : TestBase
{
    [TestMethod]
    public async Task SearchContacts_ReturnsContacts()
    {
        // Arrange
        var actions = new ContactActions(InvocationContext);
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

    [TestMethod]
    public async Task GetContact_ReturnsContact()
    {
        // Arrange
        var actions = new ContactActions(InvocationContext);
        var contactRequest = new ContactIdentifier { ContactId = "AAAAATYU54QAGI2L7BTnhA" };

        // Act
        var result = await actions.GetContact(contactRequest);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}