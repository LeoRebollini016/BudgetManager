using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Interfaces.Repositories;

namespace BudgetManager.Application.Services;

public class AccountTypesService(IAccountTypesRepository accTypesService, IUserService userService, IMapper mapper) : IAccountTypesService
{
    private readonly IAccountTypesRepository _accTypesService = accTypesService;
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task Create(AccountTypesDto accTypesDto, CancellationToken ct)
    {
        accTypesDto.UserId = _userService.GetUserId();
        var accTypes = _mapper.Map<AccountTypes>(accTypesDto);
        await _accTypesService.CreateAsync(accTypes, ct);
    }

    public async Task<bool> ExistAccTypes(string name, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        return await _accTypesService.ExistAccTypesAsync(name, userId, ct);
    }
    
    public async Task<List<AccountTypesDto>?> GetAccountTypesAsync(int userId, CancellationToken ct)
        => _mapper.Map<List<AccountTypesDto>?>(
                await _accTypesService.GetListAccountTypesAsync(userId, ct));

    public async Task Update(AccountTypesDto accountTypesDto, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        var accTypes = _mapper.Map<AccountTypes>(accountTypesDto);
        accTypes.UserId = userId;
        await _accTypesService.UpdateAsync(accTypes, ct);
    }
    public async Task<List<KeyValueDto>?> GetAccountTypesNamesAsync(CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        return await _accTypesService.GetAccountTypesNamesAsync(userId, ct) as List<KeyValueDto>;
    }
    public async Task<AccountTypesDto?> GetAccTypesById(int id, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        return await _accTypesService.GetAccTypesByIdAsync(id, userId, ct);
    }
    public async Task<bool> DeleteAccTypesById(int id, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        var accType = await _accTypesService.GetAccTypesByIdAsync(id, userId, ct);
        if (accType is null)
        {
            return false;
        }
        await _accTypesService.DeleteAccTypeAsync(id, ct);
        return true;
    }
    public async Task<bool> OrderListAccTypes(IEnumerable<int> ids, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        var accTypes = await _accTypesService.GetListAccountTypesAsync(userId, ct);

        var accTypesIds = accTypes.Select(x => x.Id);

        var IdsNotBelong = accTypesIds.Except(ids).ToList();

        if (IdsNotBelong.Any())
        {
            return false;
        }
        var sortedListAccTypes = ids.Select((value, index) =>
            new AccountTypes() { Id = value, Order = index + 1 }).AsEnumerable();

        await _accTypesService.OrderListAsync(sortedListAccTypes, ct);

        return true;
    }
}
