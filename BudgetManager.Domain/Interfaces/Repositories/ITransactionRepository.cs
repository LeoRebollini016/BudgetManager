using BudgetManager.Domain.Dtos;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(int userId);
    Task InsertTransactionAsync(TransactionCreateDto transaction);
}
