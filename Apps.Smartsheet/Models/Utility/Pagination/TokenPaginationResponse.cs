using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Pagination;

public class TokenPaginationResponse<T>
{
    [JsonProperty("lastKey")]
    public string? LastKey { get; set; }
    
    [JsonProperty("data")] 
    public IEnumerable<T> Data { get; set; } = [];
}