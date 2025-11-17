using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public OperationTypeEnum OperationTypeId { get; set; }
    public int UserId { get; set; }
}
