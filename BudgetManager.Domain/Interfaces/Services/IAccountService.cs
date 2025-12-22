using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountService
{
    Task CreateAsync(Account account, CancellationToken cancellationToken);
    Task<List<Account>> GetAccountListAsync(CancellationToken cancellationToken);
    Task DeleteAccountAsync(int accountId, CancellationToken cancellationToken);
    Task<Account?> GetAccountByIdAsync(int accountId, CancellationToken cancellationToken);
    Task UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task<List<KeyValueDto>?> GetAccountNamesAsync(CancellationToken cancellationToken);
}
