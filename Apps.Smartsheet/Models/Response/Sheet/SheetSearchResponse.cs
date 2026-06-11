using Apps.Smartsheet.Models.Entities.Sheet;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Sheet;

public record SheetSearchResponse
{
    public SheetSearchResponse(SheetSearchEntity entity)
    {
        Text = entity.Text;
        SheetId = entity.ParentObjectId;
        IsFavorite = entity.IsFavorite;
        RowId = entity.ObjectId;
    }

    [Display("Text")] 
    public string Text { get; set; }

    [Display("Sheet ID")]
    public string SheetId { get; set; }

    [Display("Row ID")]
    public string RowId { get; set; }

    [Display("Is favorite")] 
    public bool IsFavorite { get; set; }
}