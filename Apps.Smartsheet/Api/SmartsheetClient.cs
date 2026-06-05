using System.Runtime.CompilerServices;
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

    public async IAsyncEnumerable<T> PaginateOffset<T>(RestRequest request, int? timesToPaginate = null)
    {
        int currentPage = 1;
        int maxItemsPerPage = 100;
        int totalItemsYielded = 0;
        OffsetPaginationResponse<T> response;

        do
        {
            request.AddOrUpdateParameter("pageSize", maxItemsPerPage.ToString());
            request.AddOrUpdateParameter("currentPage", currentPage.ToString());

            response = await ExecuteWithErrorHandling<OffsetPaginationResponse<T>>(request);

            foreach (var item in response.Data)
            {
                totalItemsYielded++;
                yield return item;
            }

            currentPage++;

        } while (totalItemsYielded < response.TotalCount && 
                 (!timesToPaginate.HasValue || currentPage <= timesToPaginate.Value));
    }
    
    public async IAsyncEnumerable<T> PaginateToken<T>(RestRequest request, int? timesToPaginate = null)
    {
        request.AddOrUpdateParameter("paginationType", "token");
        string? lastKey = string.Empty;
        int timesPaginated = 0;

        do
        {
            timesPaginated++;
            
            if (!string.IsNullOrEmpty(lastKey))
                request.AddOrUpdateParameter("lastKey", lastKey);
            
            var response = await ExecuteWithErrorHandling<TokenPaginationResponse<T>>(request);
            lastKey = response.LastKey;
            
            foreach (var item in response.Data)
                yield return item;
            
        } while (!string.IsNullOrEmpty(lastKey) && (!timesToPaginate.HasValue || timesPaginated < timesToPaginate.Value));
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