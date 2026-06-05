namespace Apps.Smartsheet.Extensions;

public static class StringExtensions
{
    public static object ParseCellValue(this string source)
    {
        object finalValue = source; 

        if (bool.TryParse(source, out bool boolValue))
            finalValue = boolValue;
        else if (double.TryParse(source, out double numValue))
            finalValue = numValue;
        else if (DateTime.TryParse(source, out DateTime dateValue))
            finalValue = dateValue.ToString("s");

        return finalValue;
    }
}