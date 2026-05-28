using RestSharp;

namespace Apps.Smartsheet.Extensions;

public static class RestRequestExtensions
{
    public static T AddBearerTokenHeader<T>(this T request, string token) where T : RestRequest
    {
        request.AddOrUpdateHeader("Authorization", $"Bearer {token}");
        return request;
    }
}