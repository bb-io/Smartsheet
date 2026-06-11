using Apps.Smartsheet.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Smartsheet.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    private readonly IEnumerable<ConnectionPropertyValue> _baseUrlsDataItems =
    [
        new(BaseUrls.Smartsheet, "Smartsheet"),
        new(BaseUrls.SmartsheetGov, "Smartsheet Gov"),
        new(BaseUrls.SmartsheetEurope, "Smartsheet Regions Europe"),
        new(BaseUrls.SmartsheetAustralia, "Smartsheet Regions Australia"),
    ];
    
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
                    DataItems = _baseUrlsDataItems
                } 
            }
        },
        new()
        {
            Name = ConnectionTypes.OAuth,
            DisplayName = "OAuth2",
            AuthenticationType = ConnectionAuthenticationType.OAuth2,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.ClientId) { DisplayName = "Client ID" },
                new(CredsNames.ClientSecret) { DisplayName = "Client secret", Sensitive = true },
                new(CredsNames.BaseUrl)
                {
                    DisplayName = "Base URL", 
                    DataItems = _baseUrlsDataItems
                } 
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var providers = values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)).ToList();

        var connectionType = values[nameof(ConnectionPropertyGroup)] switch
        {
            var ct when ConnectionTypes.SupportedConnectionTypes.Contains(ct) => ct,
            _ => throw new Exception($"Unknown connection type: {values[nameof(ConnectionPropertyGroup)]}")
        };

        providers.Add(new AuthenticationCredentialsProvider(CredsNames.ConnectionType, connectionType));
        return providers;
    }
}