namespace BudgetManager.Domain.Dtos.Report;

public class ReportTimeSeriesDto
{
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }

}