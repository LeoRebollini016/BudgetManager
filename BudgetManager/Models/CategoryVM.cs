using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Models;

public class CategoryVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OperationTypeId { get; set; }
    public OperationTypeEnum OperationType { get; set; }
    public int UserId { get; set; }
}
