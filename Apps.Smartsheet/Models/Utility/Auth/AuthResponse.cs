using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Auth;

public class AuthResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;
}