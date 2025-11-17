namespace BudgetManager.Models;

public class TransactionDeleteVM
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; } = string.Empty;
}
