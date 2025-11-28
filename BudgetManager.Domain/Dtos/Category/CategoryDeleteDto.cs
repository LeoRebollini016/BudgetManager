using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Domain.Dtos.Category;

public class CategoryDeleteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public OperationTypeEnum OperationType { get; set; } = OperationTypeEnum.Ingreso;
}
