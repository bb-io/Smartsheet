using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class UserIdentifier
{
    [Display("User ID"), DataSource(typeof(UserDataHandler))]
    public string UserId { get; set; } = string.Empty;
}