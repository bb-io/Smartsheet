using Apps.Smartsheet.Models.Entities.Column;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Column;

public record ColumnResponse
{
    public ColumnResponse(ColumnEntity column)
    {
        Id = column.Id;
        Title = column.Title;
        Description = column.Description;
        Formula = column.Formula;
        Index = column.Index;
        Width = column.Width;
        Type = column.Type;
        IsHidden = column.IsHidden;
        IsLocked = column.IsLocked;
        IsLockedForRequestingUser = column.IsLockedForRequestingUser;
        IsPrimary = column.IsPrimary.HasValue && column.IsPrimary.Value;
    }

    [Display("Column ID")]
    public string Id { get; set; }

    [Display("Column title")]
    public string Title { get; set; }

    [Display("Column description")] 
    public string? Description { get; set; }

    [Display("Column index")]
    public int Index { get; set; }

    [Display("Column width in pixels")]
    public int Width { get; set; }

    [Display("Column formula")]
    public string? Formula { get; set; }

    [Display("Column type")]
    public string Type { get; set; }

    [Display("Is hidden")]
    public bool IsHidden { get; set; }

    [Display("Is locked")]
    public bool IsLocked { get; set; }

    [Display("Is locked for requesting user")]
    public bool IsLockedForRequestingUser { get; set; }

    [Display("Is primary")]
    public bool IsPrimary { get; set; }
}