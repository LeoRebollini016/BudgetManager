namespace BudgetManager.Domain.Dtos.Report;

public class DateRangeReportFilterDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? AccountId { get; set; }
}
