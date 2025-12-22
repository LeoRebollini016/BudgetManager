using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models;

public class AccountTypesVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El {0} es requerido.")]
    public string Name { get; set; } 
    public int UserId { get; set; }
    public int Order { get; set; }
}
