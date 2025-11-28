using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Domain.Dtos.Report;

public class ReportCategoryDto
{
    public string CategoryName { get; set; }
    public decimal Total { get; set; }
    public OperationTypeEnum OperationType { get; set; }
}
