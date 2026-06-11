using Apps.Smartsheet.Models.Entities.Children;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.Folder;

public record CreatedFolderResponse
{
    public CreatedFolderResponse(ChildEntity folder)
    {
        Id = folder.Id;
        Name = folder.Name;
        Permalink = folder.Permalink;
    }
    
    [Display("Folder ID")]
    public string Id { get; set; }

    [Display("Folder name")]
    public string Name { get; set; }

    [Display("Folder URL")]
    public string Permalink { get; set; }
}