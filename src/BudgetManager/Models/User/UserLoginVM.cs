using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.User;

public class UserLoginVM
{
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [EmailAddress(ErrorMessage = "El campo {0} debe ser un formato email válido.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
