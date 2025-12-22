using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models;

public class AccountCreateVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El {0} es requerido.")]
    public string Name { get; set; } 
    [Required(ErrorMessage = "El {0} es requerido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public double Balance { get; set; } = 0.0;
    [Required(ErrorMessage = "Debe seleccionar un tipo de cuenta.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un tipo de cuenta válido.")]
    public int AccountTypeId { get; set; }
    public IEnumerable<SelectListItem> AccountTypes { get; set; }
    [StringLength(1000, ErrorMessage = "La descripción no debe superar los 1000 caracteres.")]
    public string? Description { get; set; }
}