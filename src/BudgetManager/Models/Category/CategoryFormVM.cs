using BudgetManager.Domain.Constants.Enum;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models.Category;

public class CategoryFormVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El {0} es requerido.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Seleccioné un tipo de operación")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioné un tipo de operación válido.")]
    public int? OperationTypeId { get; set; }
    public OperationTypeEnum? OperationType { get; set; }
    public int UserId { get; set; }
}
