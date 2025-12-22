using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IAccountRepository
{
    Task CreateAsync(Account account, CancellationToken ct);
    Task<IEnumerable<Account>> GetAccountsAsync(CancellationToken ct);
    Task DeleteAccountAsync(int id, CancellationToken ct);
    Task<Account?> GetAccountByIdAsync(int id, CancellationToken ct);
    Task UpdateAccountAsync(Account id, CancellationToken ct);
    Task<IEnumerable<KeyValueDto>> GetAccountNamesAsync(CancellationToken ct);
}