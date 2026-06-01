using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class SheetIdentifier
{
    [Display("Sheet ID"), DataSource(typeof(SheetDataHandler))]
    public string SheetId { get; set; } = string.Empty;
}