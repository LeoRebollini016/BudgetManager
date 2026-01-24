namespace BudgetManager.Domain.Dtos.Transaction;

public class TransactionDto
{
    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public int OperationTypeId { get; set; }
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
    public string? Note { get; set; }
}
