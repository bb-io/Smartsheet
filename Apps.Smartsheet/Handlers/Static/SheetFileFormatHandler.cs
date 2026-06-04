using Apps.Smartsheet.Constants;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Handlers.Static;

public class SheetFileFormatHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return
        [
            new DataSourceItem(SheetFileFormats.Xlsx, "XLSX"),
            new DataSourceItem(SheetFileFormats.Csv, "CSV")
        ];
    }
}
