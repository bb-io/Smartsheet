using Apps.Smartsheet.Extensions;

namespace Apps.Smartsheet.Helper.Payload;

public static class PayloadHelper
{
    public static List<object> BuildRowPayload(List<string> columnIds, List<string> columnValues)
    {
        var cellsList = new List<object>();
        for (int i = 0; i < columnIds.Count; i++)
        {
            var cell = BuildRowPayload(columnIds[i], columnValues[i]);
            cellsList.Add(cell);
        }

        return cellsList;
    }

    public static object BuildRowPayload(string columnId, string columnValue)
    {
        if (string.IsNullOrWhiteSpace(columnId) || string.IsNullOrWhiteSpace(columnValue))
            return new {};

        if (!columnValue.StartsWith('[') || !columnValue.EndsWith(']'))
        {
            return new
            {
                columnId,
                value = columnValue.ParseCellValue(),
                strict = false
            };
        }
        
        string innerText = columnValue.Trim('[', ']');
        var items = innerText.Split(',').Select(x => x.Trim()).ToList();
        
        return new
        {
            columnId,
            objectValue = new 
            {
                objectType = "MULTI_PICKLIST", 
                values = items
            },
            strict = false 
        };

    }
}