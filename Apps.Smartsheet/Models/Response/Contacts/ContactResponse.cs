using Apps.Smartsheet.Models.Entities.Contacts;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Contacts;

public record ContactResponse
{
    public ContactResponse(ContactEntity contact)
    {
        Id = contact.Id;
        Name = contact.Name;
        Email = contact.Email;
    }

    [Display("Contact ID")]
    public string Id { get; set; }

    [Display("Contact name")]
    public string Name { get; set; }

    [Display("Contact email")]
    public string Email { get; set; }
}