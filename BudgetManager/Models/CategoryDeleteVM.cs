using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Models;

public class CategoryDeleteVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public OperationTypeEnum OperationType { get; set; }
}
