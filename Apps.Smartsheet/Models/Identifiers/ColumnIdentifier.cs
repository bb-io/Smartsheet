using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class ColumnIdentifier
{
    [Display("Column ID"), DataSource(typeof(ColumnDataHandler))]
    public string ColumnId { get; set; } = string.Empty;
}