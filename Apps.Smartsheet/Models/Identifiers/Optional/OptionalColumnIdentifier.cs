using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers.Optional;

public class OptionalColumnIdentifier
{
    [Display("Column ID"), DataSource(typeof(ColumnDataHandler))]
    public string? ColumnId { get; set; }
}