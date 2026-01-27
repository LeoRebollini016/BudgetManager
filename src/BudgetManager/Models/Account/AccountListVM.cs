using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models.Account;

public class AccountListVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; } = 0.0;
    public int AccountTypeId { get; set; }
    public string AccountType { get; set; }
    public string? Description { get; set; }
}