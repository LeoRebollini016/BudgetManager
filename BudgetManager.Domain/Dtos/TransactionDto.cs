namespace BudgetManager.Domain.Dtos;

public class TransactionDetailDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime TransactionDate { get; set; }
    public double Amount { get; set; }
    public int OperationTypeId { get; set; }
    public string OperationType { get; set; }
    public string? Note { get; set; }
    public int AccountId { get; set; }
    public string Account { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
}
