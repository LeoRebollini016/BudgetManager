using BudgetManager.Domain.Constants.Enum;

namespace BudgetManager.Web.Models.Category;

public class CategoryListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OperationTypeEnum? OperationType { get; set; }
}
