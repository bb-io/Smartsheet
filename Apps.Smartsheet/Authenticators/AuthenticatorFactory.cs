using Apps.Smartsheet.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp.Authenticators;

namespace Apps.Smartsheet.Authenticators;

public static class AuthenticatorFactory
{
    public static IAuthenticator Create(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var credsList = creds.ToList();
        string connectionType = credsList.Get(CredsNames.ConnectionType).Value;
        
        return connectionType switch
        {
            ConnectionTypes.ApiKey => new ApiTokenAuthenticator(credsList),
            ConnectionTypes.OAuth => new OAuthAuthenticator(credsList),
            _ => throw new Exception($"Unknown connection type was passed to AuthenticatorFactory: {connectionType}")
        };
    }
}