using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models;

public class AccountVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El {0} es requerido.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Debe ingresar un {0}.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public double Balance { get; set; } = 0.0;
    [Required(ErrorMessage = "Debe seleccionar una cuenta.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cuenta válida.")]
    public int AccountTypeId { get; set; }
    [StringLength(1000, ErrorMessage = "La {0} no debe superar los 1000 caracteres.")]
    public string? Description { get; set; }
}
