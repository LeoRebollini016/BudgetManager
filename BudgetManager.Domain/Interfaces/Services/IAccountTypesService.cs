using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountTypesService
{
    Task Create(AccountTypesDto accTypesDto);
    Task<bool> DeleteAccTypesById(int id);
    Task<bool> ExistAccTypes(string name);
    Task<List<AccountTypesDto>?> GetAccountTypesAsync(int userId);
    Task<AccountTypesDto?> GetAccTypesById(int id);
    Task<bool> OrderListAccTypes(IEnumerable<int> ids);
    Task Update(AccountTypesDto accountTypesDto);
}
