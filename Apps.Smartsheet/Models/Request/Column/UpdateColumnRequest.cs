using Apps.Smartsheet.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Smartsheet.Models.Request.Column;

public class UpdateColumnRequest
{
    [Display("Column title")]
    public string? Title { get; set; }
    
    [Display("Column type"), StaticDataSource(typeof(ColumnTypeDataHandler))]
    public string? Type { get; set; }
    
    [Display("Column description")]
    public string? Description { get; set; }

    [Display("Column index", Description = "Column's position in a sheet. Starts from 0")]
    public int? Index { get; set; }
    
    [Display("Column formula")]
    public string? Formula { get; set; }
    
    [Display("Column width in pixels")]
    public int? Width { get; set; }
    
    [Display("Column is hidden", Description = "False by default")]
    public bool? IsHidden { get; set; }
    
    [Display("Column is locked", Description = "False by default")]
    public bool? IsLocked { get; set; }
}