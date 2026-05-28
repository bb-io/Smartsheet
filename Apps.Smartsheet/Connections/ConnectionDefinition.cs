using Apps.Smartsheet.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Smartsheet.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = ConnectionTypes.ApiKey,
            DisplayName = "API Key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.ApiKey) { DisplayName = "API Key", Sensitive = true },
                new(CredsNames.BaseUrl)
                {
                    DisplayName = "Base URL", 
                    DataItems = 
                    [
                        new(BaseUrls.Smartsheet, "Smartsheet"),
                        new(BaseUrls.SmartsheetGov, "Smartsheet Gov"),
                        new(BaseUrls.SmartsheetEurope, "Smartsheet Regions Europe"),
                        new(BaseUrls.SmartsheetAustralia, "Smartsheet Regions Australia"),
                    ]
                } 
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values) => values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)
        ).ToList();
}