using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Domain.Dtos.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OperationTypeId { get; set; }
    public OperationTypeEnum OperationType { get; set; }
}
