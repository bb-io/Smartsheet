using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class ContactIdentifier
{
    [Display("Contact ID"), DataSource(typeof(ContactDataHandler))]
    public string ContactId { get; set; } = string.Empty;
}