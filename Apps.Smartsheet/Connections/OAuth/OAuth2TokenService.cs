using System.Globalization;
using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Utility.Auth;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Smartsheet.Connections.OAuth;

public class OAuth2TokenService(InvocationContext context) : BaseInvocable(context), IOAuth2TokenService, ITokenRefreshable
{
    private const string TokenEndpointUrl = "https://api.smartsheet.com/2.0/token";
    
    public async Task<Dictionary<string, string>> RequestToken(
        string state, 
        string code, 
        Dictionary<string, string> values, 
        CancellationToken cancellationToken)
    {
        string clientId = values.TryGetValue(CredsNames.ClientId, out var id) ? id : string.Empty;
        string clientSecret = values.TryGetValue(CredsNames.ClientSecret, out var secret) ? secret : string.Empty;
        
        var request = new RestRequest(TokenEndpointUrl, Method.Post)
            .AddParameter("grant_type", "authorization_code")
            .AddParameter("code", code)
            .AddParameter("client_id", clientId)
            .AddParameter("client_secret", clientSecret);

        return await ExecuteTokenRequest(request, cancellationToken);
    }

    public async Task<Dictionary<string, string>> RefreshToken(
        Dictionary<string, string> values, 
        CancellationToken cancellationToken)
    {
        string clientId = values.TryGetValue(CredsNames.ClientId, out var id) ? id : string.Empty;
        string clientSecret = values.TryGetValue(CredsNames.ClientSecret, out var secret) ? secret : string.Empty;
        string refreshToken = values.TryGetValue(CredsNames.RefreshToken, out var token) 
            ? token 
            : throw new Exception("Refresh token not found");
        
        var request = new RestRequest(TokenEndpointUrl, Method.Post)
            .AddParameter("grant_type", "refresh_token")
            .AddParameter("refresh_token", refreshToken)
            .AddParameter("client_id", clientId)
            .AddParameter("client_secret", clientSecret);

        return await ExecuteTokenRequest(request, cancellationToken);
    }

    public Task RevokeToken(Dictionary<string, string> values)
    {
        throw new NotImplementedException();
    }

    public bool IsRefreshToken(Dictionary<string, string> values)
    {
        return 
            values.TryGetValue(CredsNames.ExpiresIn, out var expiresIn) && 
            DateTime.UtcNow > DateTime.Parse(expiresIn, CultureInfo.InvariantCulture);
    }

    private async Task<Dictionary<string, string>> ExecuteTokenRequest(
        RestRequest request, 
        CancellationToken cancellationToken)
    {
        var client = new RestClient();
        var response = await client.ExecuteAsync(request, cancellationToken);
        if (!response.IsSuccessful)
        {
            InvocationContext.Logger?.LogError(
                $"[SmartsheetOAuth2] OAuth request token response is {response.StatusCode}. Response: {response.Content}",
                []);
            throw new Exception($"Failed to get token: {response.Content}");
        }
        
        var tokenData = JsonConvert.DeserializeObject<AuthResponse>(response.Content ?? string.Empty);
        if (tokenData is not null)
        {
            return new Dictionary<string, string>
            {
                { CredsNames.AccessToken, $"Bearer {tokenData.AccessToken}" },
                { CredsNames.RefreshToken, tokenData.RefreshToken },
                { CredsNames.ExpiresIn, DateTime.UtcNow.AddSeconds(tokenData.ExpiresIn).ToString(CultureInfo.InvariantCulture) }
            };
        }
        
        InvocationContext.Logger?.LogError(
            $"[SmartsheetOAuth2] Could not deserialize OAuth response. Response: {response.Content}",
            []);
        throw new Exception("Could not deserialize OAuth response");
    }

    public int? GetRefreshTokenExprireInMinutes(Dictionary<string, string> values)
    {
        if (!values.TryGetValue(CredsNames.ExpiresIn, out var expireValue))
            return null;

        if (!DateTime.TryParse(expireValue, CultureInfo.InvariantCulture, out var expireDate))
            return null;

        var difference = expireDate - DateTime.UtcNow;
        if (difference.TotalMinutes <= 0)
            return 0;

        return (int)difference.TotalMinutes - 5;
    }
}