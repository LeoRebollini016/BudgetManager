namespace BudgetManager.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime DateTransaction { get; set; }
    public decimal Amount { get; set; }
    public int TypeOperationId { get; set; }
    public string? Note { get; set; }
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
}
