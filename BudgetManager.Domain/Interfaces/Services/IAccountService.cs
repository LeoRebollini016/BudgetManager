using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountService
{
    Task CreateAsync(Account account, CancellationToken cancellationToken);
    Task<List<Account>> GetAccountListAsync(Guid userId, CancellationToken cancellationToken);
    Task DeleteAccountAsync(Guid userId, int accountId, CancellationToken cancellationToken);
    Task<Account?> GetAccountByIdAsync(Guid userId, int accountId, CancellationToken cancellationToken);
    Task UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task<List<KeyValueDto>?> GetAccountNamesAsync(Guid userId, CancellationToken cancellationToken);
}
