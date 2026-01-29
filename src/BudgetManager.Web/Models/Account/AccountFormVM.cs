using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.Account;

public class AccountFormVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El {0} es requerido.")]
    public string Name { get; set; } 
    [Required(ErrorMessage = "El {0} es requerido.")]
    [Range(typeof(decimal), "0,01", "79228162514264337593543950335", ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Balance { get; set; } = 0.0m;
    [Required(ErrorMessage = "Debe seleccionar un tipo de cuenta.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un tipo de cuenta válido.")]
    public int AccountTypeId { get; set; }
    public IEnumerable<SelectListItem> AccountTypes { get; set; } = [];
    [StringLength(1000, ErrorMessage = "La descripción no debe superar los 1000 caracteres.")]
    public string? Description { get; set; }
}