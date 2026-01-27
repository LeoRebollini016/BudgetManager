using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Interfaces.Repositories;

public interface IAccountTypesRepository
{
    Task CreateAsync(AccountTypes accountTypes, CancellationToken ct);
    Task<bool> ExistAccTypesAsync(Guid userId, string name, CancellationToken ct);
    Task<IEnumerable<AccountTypesDto>> GetListAccountTypesAsync(Guid userId, CancellationToken ct);
    Task<AccountTypesDto?> GetAccTypesByIdAsync(Guid userId, int id, CancellationToken ct);
    Task<AccountTypes> UpdateAsync(AccountTypes accountTypes, CancellationToken ct);
    Task DeleteAccTypeAsync(Guid userId, int id, CancellationToken ct);
    Task OrderListAsync(IEnumerable<AccountTypes> accounts, CancellationToken ct);
    Task<IEnumerable<KeyValueDto>> GetAccountTypesNamesAsync(Guid userId, CancellationToken ct);
    Task<bool> HasRelatedAccountsAsync(Guid userId, int id, CancellationToken ct);
}
