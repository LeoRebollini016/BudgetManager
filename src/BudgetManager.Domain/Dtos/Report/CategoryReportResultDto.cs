using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Domain.Dtos.Report;

public class CategoryReportResultDto
{
    public List<ReportCategoryDto> ByCategory { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
}
