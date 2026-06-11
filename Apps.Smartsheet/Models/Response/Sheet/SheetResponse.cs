using Apps.Smartsheet.Models.Entities.Sheet;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Sheet;

public record SheetResponse : CreatedSheetResponse
{
    public SheetResponse(SheetEntity entity) : base(entity)
    {
        CreatedAt = entity.CreatedAt;
        ModifiedAt = entity.ModifiedAt;
    }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Modified at")]
    public DateTime ModifiedAt { get; set; }
}