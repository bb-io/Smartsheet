using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Extensions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;
using RestSharp.Authenticators;

namespace Apps.Smartsheet.Authenticators;

public class ApiTokenAuthenticator(IEnumerable<AuthenticationCredentialsProvider> creds) : IAuthenticator
{
    public ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        string apiKey = creds.Get(CredsNames.ApiKey).Value;
        request.AddBearerTokenHeader(apiKey);
        return ValueTask.CompletedTask;
    }
}