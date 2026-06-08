using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Contacts;

public class ContactEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Name} ({Email})";
    }
}