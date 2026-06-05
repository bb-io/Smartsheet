using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Models.Identifiers;

public class RowIdentifier
{
    [Display("Row ID"), DataSource(typeof(RowDataHandler))]
    public string RowId { get; set; } = string.Empty;
}