using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Dtos.Report;

public class MonthlyReportResultDto
{
    public List<ReportTimeSeriesDto> TimeSeries { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
}
