using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Utility.Wrapper;

public class ResultWrapper<T>
{
    [JsonProperty("result")]
    public T Result { get; set; }
}