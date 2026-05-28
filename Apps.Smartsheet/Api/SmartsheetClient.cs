using Apps.Smartsheet.Authenticators;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Utility.Error;
using Apps.Smartsheet.Models.Utility.Pagination;
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

    public async IAsyncEnumerable<T> Paginate<T>(RestRequest request)
    {
        int currentPage = 1;
        int maxItemsPerPage = 100;
        int totalItemsYielded = 0;
        PaginationResponse<T> response;

        do
        {
            request.AddOrUpdateParameter("pageSize", maxItemsPerPage.ToString());
            request.AddOrUpdateParameter("currentPage", currentPage.ToString());

            response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request);

            foreach (var item in response.Data)
            {
                totalItemsYielded++;
                yield return item;
            }

            currentPage++;

        } while (totalItemsYielded < response.TotalCount);
    }
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        string statusPart = $"Status code {response.StatusCode} ({(int)response.StatusCode}).";
        if (!string.IsNullOrWhiteSpace(response.ErrorMessage))
            return new PluginApplicationException($"{statusPart} {response.ErrorMessage}");

        if (string.IsNullOrWhiteSpace(response.Content))
            return new PluginApplicationException($"{statusPart} The server did not return any content");
        
        var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        if (error is null || string.IsNullOrEmpty(error.Message))
            return new PluginApplicationException($"{statusPart} Could not deserialize the error. Raw: {response.Content}");

        return new PluginApplicationException(error.Message);
    }
}