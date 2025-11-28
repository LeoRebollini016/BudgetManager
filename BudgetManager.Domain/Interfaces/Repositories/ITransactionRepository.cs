using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(int userId);
    Task InsertTransactionAsync(TransactionCreateDto transaction);
    Task UpdateTransactionAsync(TransactionCreateDto transaction);
    Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(int transactionId, int userId);
    Task DeleteTransactionByIdAsync(int transactionId, int userId);
}
