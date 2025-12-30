using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.AccountTypes;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountTypesService
{
    Task Create(Guid userId, AccountTypesDto accTypesDto, CancellationToken ct);
    Task<bool> DeleteAccTypesById(Guid userId, int id, CancellationToken ct);
    Task<bool> ExistAccTypes(Guid userId, string name, CancellationToken ct);
    Task<List<AccountTypesDto>?> GetAccountTypesAsync(Guid userId, CancellationToken ct);
    Task<List<KeyValueDto>?> GetAccountTypesNamesAsync(Guid userId, CancellationToken ct);
    Task<AccountTypesDto?> GetAccTypesById(Guid userId, int id, CancellationToken ct);
    Task<bool> OrderListAccTypes(Guid userId, IEnumerable<int> ids, CancellationToken ct);
    Task Update(Guid userId, AccountTypesDto accountTypesDto, CancellationToken ct);
}
