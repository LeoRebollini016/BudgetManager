namespace BudgetManager.Domain.Dtos.Report;

public class MonthlyReportFilterDto
{
    public DateTime Month { get; set; }
    public int? AccountId { get; set; }
}
