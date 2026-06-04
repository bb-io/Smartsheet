using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Wrapper;

public class ResultWrapper<T>
{
    [JsonProperty("result")]
    public T Result { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    public bool IsSuccessfulResponse => Message == "SUCCESS";
}