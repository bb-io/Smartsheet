using Apps.Smartsheet.Constants;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Smartsheet.Extensions;

public static class FileReferenceExtensions
{
    public static string GetSheetContentType(this FileReference fileReference)
    {
        return fileReference.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) 
            ? SheetFileFormats.Csv
            : SheetFileFormats.XlsxSheet; 
    }
}