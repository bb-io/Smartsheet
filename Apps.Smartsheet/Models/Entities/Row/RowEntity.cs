using Apps.Smartsheet.Models.Entities.Cell;
using Newtonsoft.Json;

namespace Apps.Smartsheet.Models.Entities.Row;

public class RowEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("rowNumber")]
    public int RowNumber { get; set; }

    [JsonProperty("cells")]
    public List<CellEntity> Cells { get; set; } = [];

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("modifiedAt")]
    public DateTime? ModifiedAt { get; set; }

    public override string ToString()
    {
        var validCells = Cells
            .Where(c => !string.IsNullOrWhiteSpace(c.Value))
            .Select(c => c.Value)
            .ToList();

        if (validCells.Count == 0)
            return $"Row {RowNumber}";

        return $"Row {RowNumber}: {string.Join("; ", validCells)}";
    }
}