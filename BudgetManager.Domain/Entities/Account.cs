namespace BudgetManager.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    public int AccountTypeId { get; set; }
    public string? Description { get; set; }
    public bool IsClosed { get; set; }
}
