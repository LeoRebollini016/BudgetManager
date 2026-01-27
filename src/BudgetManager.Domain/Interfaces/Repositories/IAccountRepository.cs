using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IAccountRepository
{
    Task CreateAsync(Account account, CancellationToken ct);
    Task<IEnumerable<AccountDto>> GetAccountsAsync(Guid userId, CancellationToken ct);
    Task DeleteAccountAsync(Guid userId, int id, CancellationToken ct);
    Task<Account?> GetAccountByIdAsync(Guid userId, int id, CancellationToken ct);
    Task UpdateAccountAsync(Account id, CancellationToken ct);
    Task<IEnumerable<KeyValueDto>> GetAccountNamesAsync(Guid userId, CancellationToken ct);
    Task<bool> ExistsByNameAsync(Guid userId, string name, CancellationToken ct, int? accountId = null);
}