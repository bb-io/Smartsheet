using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Wrapper;

public class Result<T> : Result
{
    [JsonProperty("result")]
    public T Value { get; set; }
}


public class Result
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    public bool IsSuccessfulResponse => Message == "SUCCESS";
}