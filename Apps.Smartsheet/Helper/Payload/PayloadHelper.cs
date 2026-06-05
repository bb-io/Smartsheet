using Apps.Smartsheet.Extensions;

namespace Apps.Smartsheet.Helper.Payload;

public static class PayloadHelper
{
    public static List<object> BuildRowPayload(List<string> columnIds, List<string> columnValues)
    {
        var cellsList = new List<object>();
        for (int i = 0; i < columnIds.Count; i++)
        {
            object value = columnValues[i].ParseCellValue();
            cellsList.Add(new
            {
                columnId = long.Parse(columnIds[i]),
                value = value,
                strict = false 
            });
        }

        return cellsList;
    }
}