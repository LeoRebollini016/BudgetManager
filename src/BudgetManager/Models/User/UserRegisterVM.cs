using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models;

public class UserRegisterVM
{
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")]
    public string Password { get; set; }
    [Required, DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; }
}