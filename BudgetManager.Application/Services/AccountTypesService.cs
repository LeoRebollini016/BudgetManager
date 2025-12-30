using AutoMapper;
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

    public async Task Create(Guid userId, AccountTypesDto accTypesDto, CancellationToken ct)
    {
        var accTypes = _mapper.Map<AccountTypes>(accTypesDto);
        accTypes.UserId = userId;
        await _accTypesService.CreateAsync(accTypes, ct);
    }

    public async Task<bool> ExistAccTypes(Guid userId, string name, CancellationToken ct)
        => await _accTypesService.ExistAccTypesAsync(userId, name, ct);
    
    public async Task<List<AccountTypesDto>?> GetAccountTypesAsync(Guid userId, CancellationToken ct)
        => _mapper.Map<List<AccountTypesDto>?>(
                await _accTypesService.GetListAccountTypesAsync(userId, ct));

    public async Task Update(Guid userId, AccountTypesDto accountTypesDto, CancellationToken ct)
    {
        var accTypes = _mapper.Map<AccountTypes>(accountTypesDto);
        accTypes.UserId = userId;
        await _accTypesService.UpdateAsync(accTypes, ct);
    }
    public async Task<List<KeyValueDto>?> GetAccountTypesNamesAsync(Guid userId, CancellationToken ct)
        => await _accTypesService.GetAccountTypesNamesAsync(userId, ct) as List<KeyValueDto>;
    public async Task<AccountTypesDto?> GetAccTypesById(Guid userId, int id, CancellationToken ct)
        => await _accTypesService.GetAccTypesByIdAsync(userId, id, ct);
    public async Task<bool> DeleteAccTypesById(Guid userId, int id, CancellationToken ct)
    {
        var accType = await _accTypesService.GetAccTypesByIdAsync(userId, id, ct);
        if (accType is null)
        {
            return false;
        }
        await _accTypesService.DeleteAccTypeAsync(userId, id, ct);
        return true;
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
