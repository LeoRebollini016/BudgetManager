using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.AccountTypes;

public class AccountTypesFormVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El {0} es requerido.")]
    public string Name { get; set; }
    public int UserId { get; set; }
    public int Order { get; set; }
}
