using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Smartsheet.Handlers.Static;

public class ColumnTypeDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return
        [
            new DataSourceItem("CHECKBOX", "Checkbox"),
            new DataSourceItem("CONTACT_LIST", "Contact list"),
            new DataSourceItem("MULTI_CONTACT_LIST", "Contact list (Multi)"),
            new DataSourceItem("DATE", "Date"),
            new DataSourceItem("ABSTRACT_DATETIME", "Project Date/Time (Abstract)"),
            new DataSourceItem("DATETIME", "Date/Time"),
            new DataSourceItem("PICKLIST", "Dropdown list"),
            new DataSourceItem("MULTI_PICKLIST", "Dropdown list (Multi)"),
            new DataSourceItem("DURATION", "Duration"),
            new DataSourceItem("PREDECESSOR", "Predecessor"),
            new DataSourceItem("TEXT_NUMBER", "Text/Number")
        ];
    }
}