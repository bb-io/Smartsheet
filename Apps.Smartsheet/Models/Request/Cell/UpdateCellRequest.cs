using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Cell;

public class UpdateCellRequest
{
    [Display("New value")] 
    public string NewValue { get; set; } = string.Empty;
}