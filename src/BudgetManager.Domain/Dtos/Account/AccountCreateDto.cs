namespace BudgetManager.Domain.Dtos.Account;

public class AccountCreateDto
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public int AccountTypeId { get; set; }
    public string? Description { get; set; }
}
