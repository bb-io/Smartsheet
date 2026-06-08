using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.User;

public class MinimalUserEntity
{
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}