using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Contacts;

public class SearchContactsRequest
{
    [Display("User full name contains")]
    public string? NameContains { get; set; }
    
    [Display("Email contains")]
    public string? EmailContains { get; set; }
}