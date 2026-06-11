using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Error;

public class ErrorResponse
{
    [JsonProperty("message")]
    public string? Message { get; set; }
}