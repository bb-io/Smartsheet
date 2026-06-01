namespace Apps.Smartsheet.Helper.Validation.Interfaces;

public interface IHasCreatedDate : IDateFilter
{
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
}