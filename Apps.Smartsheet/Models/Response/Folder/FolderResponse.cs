using Apps.Smartsheet.Models.Entities.Folder;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Folder;

public record FolderResponse
{
    public FolderResponse(FolderEntity folder)
    {
        Id = folder.Id;
        Name = folder.Name;
        Permalink = folder.Permalink;
        CreatedAt = folder.CreatedAt;
        ModifiedAt = folder.ModifiedAt;
    }

    [Display("Folder ID")]
    public string Id { get; set; }

    [Display("Folder name")]
    public string Name { get; set; }

    [Display("Folder URL")]
    public string Permalink { get; set; }

    [Display("Created at")] 
    public DateTime CreatedAt { get; set; }

    [Display("Modified at")] 
    public DateTime? ModifiedAt { get; set; }
}