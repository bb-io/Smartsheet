using Apps.Smartsheet.Helper.Validation.Interfaces;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Smartsheet.Helper.Validation;

public static class DateInputValidator
{
    public static void ValidateDates(this IDateFilter input)
    {
        List<string> errors = [];
        
        if (input is IHasCreatedDate c &&
            c.CreatedAfter.HasValue && c.CreatedBefore.HasValue &&
            c.CreatedAfter > c.CreatedBefore)
        {
            errors.Add("'Created after' date cannot be later than 'Created before' date");
        }

        if (input is IHasModifiedDate p &&
            p.ModifiedAfter.HasValue && p.ModifiedBefore.HasValue &&
            p.ModifiedAfter > p.ModifiedBefore)
        {
            errors.Add("'Modified after' date cannot be later than 'Modified before' date");
        }

        if (errors.Count != 0)
            throw new PluginMisconfigurationException(string.Join(". ", errors));
    }
}