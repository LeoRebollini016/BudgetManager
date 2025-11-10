using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IAccountRepository
{
    Task CreateAsync(Account account);
    Task<IEnumerable<Account>> GetAccountsAsync();
    Task DeleteAccountAsync(int id);
    Task<Account?> GetAccountByIdAsync(int id);
    Task UpdateAccountAsync(Account id);
    Task<IEnumerable<KeyValueDto>> GetAccountNamesAsync(int userId);
}