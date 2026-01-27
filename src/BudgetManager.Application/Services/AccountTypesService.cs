using AutoMapper;
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Interfaces.Repositories;

namespace BudgetManager.Application.Services;

public class AccountTypesService(IAccountTypesRepository accTypesService, IMapper mapper) : IAccountTypesService
{
    private readonly IAccountTypesRepository _accTypesService = accTypesService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Create(Guid userId, AccountTypesDto accountTypesDto, CancellationToken ct)
    {
        var isDuplicate = await _accTypesService.ExistAccTypesAsync(userId, accountTypesDto.Name, ct);
        if (isDuplicate)
        {
            return Result.Fail("Ya tiene un tipo de cuenta con el mismo nombre.", nameof(accountTypesDto.Name));
        }
        var accTypes = _mapper.Map<AccountTypes>(accountTypesDto);
        accTypes.UserId = userId;
        await _accTypesService.CreateAsync(accTypes, ct);
        return Result.Ok();
    }
  
    public async Task<List<AccountTypesDto>?> GetAccountTypesAsync(Guid userId, CancellationToken ct)
        => _mapper.Map<List<AccountTypesDto>?>(
                await _accTypesService.GetListAccountTypesAsync(userId, ct));

    public async Task<Result> Update(Guid userId, AccountTypesDto accountTypesDto, CancellationToken ct)
    {
        var existing = await _accTypesService.GetAccTypesByIdAsync(userId, accountTypesDto.Id, ct);
        if (existing is null)
        {
            return Result.Fail("El tipo de cuenta no existe o no tienes permisos.", null);
        }
        var isDuplicate = await _accTypesService.ExistAccTypesAsync(userId, accountTypesDto.Name, ct);
        if (isDuplicate)
        {
            return Result.Fail("Ya tiene un tipo de cuenta con el mismo nombre.", nameof(accountTypesDto.Name));
        }
        var accTypes = _mapper.Map<AccountTypes>(accountTypesDto);
        accTypes.UserId = userId;
        await _accTypesService.UpdateAsync(accTypes, ct);
        return Result.Ok();
    }
    public async Task<List<KeyValueDto>?> GetAccountTypesNamesAsync(Guid userId, CancellationToken ct)
        => await _accTypesService.GetAccountTypesNamesAsync(userId, ct) as List<KeyValueDto>;
    public async Task<AccountTypesDto?> GetAccTypesById(Guid userId, int id, CancellationToken ct)
        => await _accTypesService.GetAccTypesByIdAsync(userId, id, ct);
    public async Task<Result> DeleteAccTypesById(Guid userId, int id, CancellationToken ct)
    {
        var existing = await _accTypesService.GetAccTypesByIdAsync(userId, id, ct);
        if (existing is null)
        {
            return Result.Fail("El tipo de cuenta no existe o no tienes permisos.", null);
        }
        var hasRelatedAccounts = await _accTypesService.HasRelatedAccountsAsync(userId, id, ct);
        if (hasRelatedAccounts)
        {
            return Result.Fail("No se puede eliminar el tipo de cuenta porque forma parte del historial de tus cuentas.", string.Empty);
        }
        await _accTypesService.DeleteAccTypeAsync(userId, id, ct);
        return Result.Ok();
    }
    public async Task<bool> OrderListAccTypes(Guid userId, IEnumerable<int> ids, CancellationToken ct)
    {
        var accTypes = await _accTypesService.GetListAccountTypesAsync(userId, ct);

        var accTypesIds = accTypes.Select(x => x.Id);

        var IdsNotBelong = accTypesIds.Except(ids).ToList();

        if (IdsNotBelong.Any())
        {
            return false;
        }
        var sortedListAccTypes = ids.Select((value, index) =>
            new AccountTypes() { Id = value, Order = index + 1, UserId = userId }).AsEnumerable();

        await _accTypesService.OrderListAsync(sortedListAccTypes, ct);

        return true;
    }
}
