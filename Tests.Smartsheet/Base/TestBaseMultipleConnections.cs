using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;
using Newtonsoft.Json;

namespace Tests.Smartsheet.Base;

public class TestBaseMultipleConnections : TestBase
{
    public new TestContext TestContext
    {
        get => base.TestContext!;
        set => base.TestContext = value;
    }

    protected void PrintJsonResult(object result)
    {
        TestContext?.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
    }

    protected void PrintDataHandlerResult(IEnumerable<DataSourceItem> items)
    {
        var itemsList = items.ToList();

        TestContext?.WriteLine($"Count: {itemsList.Count}");
        foreach (var item in itemsList)
            TestContext?.WriteLine($"{item.Value} - {item.DisplayName}");
    }
    
    protected void PrintFileFolderPickerResult(IEnumerable<FileDataItem> items)
    {
        var itemsList = items.ToList();

        TestContext?.WriteLine($"Count: {itemsList.Count}");
        foreach (var item in itemsList)
        {
            string selectable = item.IsSelectable ? "Selectable" : "Not selectable";
            string type = item.Type == 0 ? "folder" : "file";
            TestContext?.WriteLine($"[{selectable} {type}] {item.Id} - {item.DisplayName} - {item.Date}");
        }
    }
}