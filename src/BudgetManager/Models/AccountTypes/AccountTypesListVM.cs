using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.AccountTypes;

public class AccountTypesListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
}
