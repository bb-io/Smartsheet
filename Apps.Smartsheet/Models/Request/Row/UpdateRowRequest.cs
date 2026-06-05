using Apps.Smartsheet.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Smartsheet.Models.Request.Row;

public class UpdateRowRequest
{
    [Display("Column IDs"), DataSource(typeof(ColumnDataHandler))]
    public List<string> ColumnIds { get; set; } = [];

    [Display("Column values", Description = "Corresponds to the 'Column IDs' input")]
    public List<string> ColumnValues { get; set; } = [];
    
    public UpdateRowRequest Validate()
    {
        if (ColumnIds.Count != ColumnValues.Count)
            throw new PluginMisconfigurationException(
                "The 'Column values' input must correspond to the 'Column IDs' input. " +
                "They must have the same number of items in the same order.");
        
        return this;
    }
}