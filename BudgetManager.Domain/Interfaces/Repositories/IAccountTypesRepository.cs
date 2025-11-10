using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Interfaces.Repositories;

public interface IAccountTypesRepository
{
    Task CreateAsync(AccountTypes accountTypes);
    Task<bool> ExistAccTypesAsync(string name, int userId);
    Task<IEnumerable<AccountTypesDto>> GetListAccountTypesAsync(int userId);
    Task<AccountTypesDto?> GetAccTypesByIdAsync(int id, int userId);
    Task<AccountTypes> UpdateAsync(AccountTypes accountTypes);
    Task DeleteAccTypeAsync(int id);
    Task OrderListAsync(IEnumerable<AccountTypes> accounts);
    Task<IEnumerable<KeyValueDto>> GetAccountTypesNamesAsync(int userId);
}
