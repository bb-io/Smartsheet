namespace Apps.Smartsheet.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> WhereContains<T>(
        this IEnumerable<T> source,
        Func<T, string?> selector,
        string? substring)
    {
        if (string.IsNullOrWhiteSpace(substring))
            return source;

        return source.Where(x => selector(x)?.Contains(substring, StringComparison.OrdinalIgnoreCase) == true);
    }
    
    public static IAsyncEnumerable<T> WhereContains<T>(
        this IAsyncEnumerable<T> source,
        Func<T, string?> selector,
        string? substring)
    {
        if (string.IsNullOrWhiteSpace(substring))
            return source;

        return source.Where(x => selector(x)?.Contains(substring, StringComparison.OrdinalIgnoreCase) == true);
    }
}