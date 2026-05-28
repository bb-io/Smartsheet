using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Pagination;

public class OffsetPaginationResponse<T>
{
    [JsonProperty("pageNumber")]
    public int PageNumber { get; set; }
    
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    [JsonProperty("totalPages")]
    public int TotalPages { get; set; }

    [JsonProperty("totalCount")]
    public int TotalCount { get; set; }

    [JsonProperty("data")]
    public IEnumerable<T> Data { get; set; } = [];
}