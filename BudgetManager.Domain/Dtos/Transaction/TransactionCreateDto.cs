namespace BudgetManager.Domain.Dtos.Transaction;

public class TransactionCreateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime TransactionDate { get; set; }
    public double Amount { get; set; }
    public string? Note { get; set; }
    public int AccountId { get; set; }
    public int OperationTypeId { get; set; }
    public int CategoryId { get; set; }
}
