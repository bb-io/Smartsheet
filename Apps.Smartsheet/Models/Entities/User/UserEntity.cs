using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.User;

public class UserEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonProperty("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("admin")]
    public bool IsAdmin { get; set; }

    [JsonProperty("groupAdmin")]
    public bool IsGroupAdmin { get; set; }

    [JsonProperty("isInternal")]
    public bool IsInternal { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Email})";
    }
}