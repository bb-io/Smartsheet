using Apps.Smartsheet.Models.Entities.Sheet;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Sheet;

public record CreatedSheetResponse
{
    public CreatedSheetResponse(SheetEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        AccessLevel = entity.AccessLevel;
        Permalink = entity.Permalink;
    }

    [Display("Sheet ID")] 
    public string Id { get; set; }

    [Display("Sheet name")]
    public string Name { get; set; }

    [Display("Sheet access level")]
    public string AccessLevel { get; set; }

    [Display("Sheet URL")]
    public string Permalink { get; set; }
}