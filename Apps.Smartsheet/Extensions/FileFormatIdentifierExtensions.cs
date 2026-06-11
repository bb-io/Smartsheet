using Apps.Smartsheet.Constants;
using Apps.Smartsheet.Models.Identifiers;

namespace Apps.Smartsheet.Extensions;

public static class FileFormatIdentifierExtensions
{
    public static string GetFileExtension(this FileFormatIdentifier identifier)
    {
        return identifier.FileFormat switch
        {
            SheetFileFormats.Xlsx => ".xlsx",
            SheetFileFormats.Csv => ".csv",
            _ => ".xlsx",
        };
    }
}