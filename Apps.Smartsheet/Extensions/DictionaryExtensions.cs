namespace Apps.Smartsheet.Extensions;

public static class DictionaryExtensions
{
    public static Dictionary<string, object?> AddIfNotEmpty(
        this Dictionary<string, object?> body, 
        string key,
        object? value)
    {
        if (value is null || string.IsNullOrWhiteSpace(key))
            return body;
        
        body.Add(key, value);
        return body;
    }
}