using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Tests.Smartsheet.Base;

public class TestBase
{
    public IEnumerable<AuthenticationCredentialsProvider> Creds { get; set; }

    public InvocationContext InvocationContext { get; set; }

    public FileManager FileManager { get; set; }

    public TestBase()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        Creds = config.GetSection("ConnectionDefinition").GetChildren()
            .Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)).ToList();


        var relativePath = config.GetSection("TestFolder").Value;
        var projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        var folderLocation = Path.Combine(projectDirectory, relativePath);

        InvocationContext = new InvocationContext
        {
            AuthenticationCredentialsProviders = Creds,
        };

        FileManager = new FileManager();
    }

    protected static void PrintJsonResult(object result)
    {
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
    }

    protected static void PrintDataHandlerResult(IEnumerable<DataSourceItem> items)
    {
        var itemsList = items.ToList();

        Console.WriteLine($"Count: {itemsList.Count}");
        foreach (var item in itemsList)
            Console.WriteLine($"{item.Value} - {item.DisplayName}");
    }
    
    protected static void PrintFileFolderPickerResult(IEnumerable<FileDataItem> items)
    {
        var itemsList = items.ToList();

        Console.WriteLine($"Count: {itemsList.Count}");
        foreach (var item in itemsList)
        {
            string selectable = item.IsSelectable ? "Selectable" : "Not selectable";
            string type = item.Type == 0 ? "folder" : "file";
            Console.WriteLine($"[{selectable} {type}] {item.Id} - {item.DisplayName} - {item.Date}");
        }
    }
}
