using Apps.Smartsheet.Authenticators;
using Apps.Smartsheet.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Smartsheet.Api;

public class SmartsheetClient(List<AuthenticationCredentialsProvider> creds) : BlackBirdRestClient(new() 
{
    BaseUrl = new Uri(creds.Get(CredsNames.BaseUrl).Value),
    Authenticator = new ApiTokenAuthenticator(creds)
})
{
    public SmartsheetClient(IEnumerable<AuthenticationCredentialsProvider> creds) : this(creds.ToList()) { }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var error = JsonConvert.DeserializeObject(response.Content);
        var errorMessage = "";

        throw new PluginApplicationException(errorMessage);
    }
}