using BudgetManager.Domain.Constants.Enum;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.Category;

public class CategoryDeleteVM
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public OperationTypeEnum OperationType { get; set; }
}
