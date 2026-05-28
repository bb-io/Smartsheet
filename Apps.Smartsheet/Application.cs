using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Smartsheet;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.TaskManagement, ApplicationCategory.DatabaseAndSpreadsheet];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}