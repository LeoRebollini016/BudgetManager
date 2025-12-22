using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.AccountTypes;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountTypesService
{
    Task Create(AccountTypesDto accTypesDto, CancellationToken ct);
    Task<bool> DeleteAccTypesById(int id, CancellationToken ct);
    Task<bool> ExistAccTypes(string name, CancellationToken ct);
    Task<List<AccountTypesDto>?> GetAccountTypesAsync(int userId, CancellationToken ct);
    Task<List<KeyValueDto>?> GetAccountTypesNamesAsync(CancellationToken ct);
    Task<AccountTypesDto?> GetAccTypesById(int id, CancellationToken ct);
    Task<bool> OrderListAccTypes(IEnumerable<int> ids, CancellationToken ct);
    Task Update(AccountTypesDto accountTypesDto, CancellationToken ct);
}
