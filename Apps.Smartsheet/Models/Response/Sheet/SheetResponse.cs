using Apps.Smartsheet.Models.Entities.Sheet;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Sheet;

public record SheetResponse
{
    public SheetResponse(SheetEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        AccessLevel = entity.AccessLevel;
        CreatedAt = entity.CreatedAt;
        ModifiedAt = entity.ModifiedAt;
        Permalink = entity.Permalink;
    }

    [Display("Sheet ID")] 
    public string Id { get; set; }

    [Display("Sheet name")]
    public string Name { get; set; }

    [Display("Sheet access level")]
    public string AccessLevel { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Modified at")]
    public DateTime ModifiedAt { get; set; }

    [Display("Sheet URL")]
    public string Permalink { get; set; }
}