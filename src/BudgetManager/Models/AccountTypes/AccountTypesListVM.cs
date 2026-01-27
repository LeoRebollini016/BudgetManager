using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models.AccountTypes;

public class AccountTypesListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
}
