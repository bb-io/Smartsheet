namespace Apps.Smartsheet.Extensions;

public static class StringExtensions
{
    public static bool MatchesSearch(this string? source, string? searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        
        return source?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true;
    }
}