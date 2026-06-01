using Apps.Smartsheet.Helper.Validation.Interfaces;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Request.Sheet;

public class SearchSheetsRequest : IHasCreatedDate, IHasModifiedDate
{
    [Display("Sheet name contains")]
    public string? NameContains { get; set; }

    [Display("Created after")]
    public DateTime? CreatedAfter { get; set; }
    
    [Display("Created before")]
    public DateTime? CreatedBefore { get; set; }
    
    [Display("Modified after")]
    public DateTime? ModifiedAfter { get; set; }
    
    [Display("Modified before")]
    public DateTime? ModifiedBefore { get; set; }
}