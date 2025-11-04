namespace BudgetManager.Domain.Dtos;

public class AccountCreateDto
{
    public string Name { get; set; }
    public double Balance { get; set; }
    public int AccountTypeId { get; set; }
    public string? Description { get; set; }
}
