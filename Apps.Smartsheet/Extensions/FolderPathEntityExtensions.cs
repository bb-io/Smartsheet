using Apps.Smartsheet.Models.Entities.Folder;

namespace Apps.Smartsheet.Extensions;

public static class FolderPathEntityExtensions
{
    public static List<FolderPathEntity> GetPathEntities(this FolderPathEntity pathEntity)
    {
        var folders = new List<FolderPathEntity>();
        var currentNode = pathEntity;

        while (currentNode != null)
        {
            folders.Add(currentNode); 
            currentNode = currentNode.Folders?.FirstOrDefault();
        }

        return folders;
    }
}