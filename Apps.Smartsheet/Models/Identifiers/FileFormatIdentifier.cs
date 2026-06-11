using Apps.Smartsheet.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Smartsheet.Models.Identifiers;

public class FileFormatIdentifier
{
    [Display("File format", Description = "XLSX is the default value")] 
    [StaticDataSource(typeof(SheetFileFormatHandler))]
    public string? FileFormat { get; set; }
}