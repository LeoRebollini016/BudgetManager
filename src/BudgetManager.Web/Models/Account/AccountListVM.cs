using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.Account;

public class AccountListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; } = 0.0m;
    public int AccountTypeId { get; set; }
    public string AccountType { get; set; }
    public string? Description { get; set; }
}