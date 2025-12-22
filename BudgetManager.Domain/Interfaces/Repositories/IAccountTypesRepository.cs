using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Interfaces.Repositories;

public interface IAccountTypesRepository
{
    Task CreateAsync(AccountTypes accountTypes, CancellationToken ct);
    Task<bool> ExistAccTypesAsync(string name, int userId, CancellationToken ct);
    Task<IEnumerable<AccountTypesDto>> GetListAccountTypesAsync(int userId, CancellationToken ct);
    Task<AccountTypesDto?> GetAccTypesByIdAsync(int id, int userId, CancellationToken ct);
    Task<AccountTypes> UpdateAsync(AccountTypes accountTypes, CancellationToken ct);
    Task DeleteAccTypeAsync(int id, CancellationToken ct);
    Task OrderListAsync(IEnumerable<AccountTypes> accounts, CancellationToken ct);
    Task<IEnumerable<KeyValueDto>> GetAccountTypesNamesAsync(int userId, CancellationToken ct);
}
