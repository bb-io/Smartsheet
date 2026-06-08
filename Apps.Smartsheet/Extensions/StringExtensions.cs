namespace Apps.Smartsheet.Extensions;

public static class StringExtensions
{
    public static object ParseCellValue(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) 
            return value;

        if (bool.TryParse(value, out bool boolValue)) 
            return boolValue;
    
        if (double.TryParse(value, out double numValue)) 
            return numValue;
    
        if (DateTime.TryParse(value, out DateTime dateValue)) 
            return dateValue.ToString("s");

        return value;
    }
}