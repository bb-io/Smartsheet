using Apps.Smartsheet.Models.Entities.Folder;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Folder;

public record FolderResponse : CreatedFolderResponse
{
    public FolderResponse(FolderEntity folder) : base(folder)
    {
        CreatedAt = folder.CreatedAt;
        ModifiedAt = folder.ModifiedAt;
    }

    [Display("Created at")] 
    public DateTime CreatedAt { get; set; }

    [Display("Modified at")] 
    public DateTime? ModifiedAt { get; set; }
}