namespace Apps.Smartsheet.Helper.Validation.Interfaces;

public interface IHasModifiedDate : IDateFilter
{
    public DateTime? ModifiedAfter { get; set; }
    public DateTime? ModifiedBefore { get; set; }
}