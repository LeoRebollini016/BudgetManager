namespace BudgetManager.Domain.Dtos.Account;

public class AccountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    public int AccountTypeId { get; set; }
    public string? Description { get; set; }
}
