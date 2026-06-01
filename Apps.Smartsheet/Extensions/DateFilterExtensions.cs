using Apps.Smartsheet.Helper.Validation.Interfaces;

namespace Apps.Smartsheet.Extensions;

public static class DateFilterExtensions
{
    public static IAsyncEnumerable<T> ApplyCreatedDateFilter<T>(
        this IAsyncEnumerable<T> source,
        Func<T, DateTime?> dateSelector,
        IHasCreatedDate filter)
    {
        if (filter.CreatedAfter.HasValue)
            source = source.Where(x => dateSelector(x) >= filter.CreatedAfter.Value);

        if (filter.CreatedBefore.HasValue)
            source = source.Where(x => dateSelector(x) <= filter.CreatedBefore.Value);

        return source;
    }

    public static IAsyncEnumerable<T> ApplyModifiedDateFilter<T>(
        this IAsyncEnumerable<T> source,
        Func<T, DateTime?> dateSelector,
        IHasModifiedDate filter)
    {
        if (filter.ModifiedAfter.HasValue)
            source = source.Where(x => dateSelector(x) >= filter.ModifiedAfter.Value);

        if (filter.ModifiedBefore.HasValue)
            source = source.Where(x => dateSelector(x) <= filter.ModifiedBefore.Value);

        return source;
    }
}