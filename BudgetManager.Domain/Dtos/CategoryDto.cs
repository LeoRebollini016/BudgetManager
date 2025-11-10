using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Domain.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OperationTypeId { get; set; }
    public OperationTypeEnum OperationType { get; set; }
    public int userId { get; set; }
}
