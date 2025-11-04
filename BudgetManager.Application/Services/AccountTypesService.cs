using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Interfaces.Repositories;

namespace BudgetManager.Application.Services;

public class AccountTypesService(IAccountTypesRepositories accTypesService, IUserService userService, IMapper mapper) : IAccountTypesService
{
    private readonly IAccountTypesRepositories _accTypesService = accTypesService;
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task Create(AccountTypesDto accTypesDto)
    {
        accTypesDto.UserId = _userService.GetUserId();
        var accTypes = _mapper.Map<AccountTypes>(accTypesDto);
        await _accTypesService.CreateAsync(accTypes);
    }

    public async Task<bool> ExistAccTypes(string name)
    {
        var userId = _userService.GetUserId();
        return await _accTypesService.ExistAccTypesAsync(name, userId);
    }
 
    public async Task<List<AccountTypesDto>?> GetAccountTypesAsync(int userId)
        => _mapper.Map<List<AccountTypesDto>?>(
                await _accTypesService.GetListAccountTypesAsync(userId));

    public async Task Update(AccountTypesDto accountTypesDto)
    {
        var userId = _userService.GetUserId();
        var accTypes = _mapper.Map<AccountTypes>(accountTypesDto);
        accTypes.Id = userId;
        await _accTypesService.UpdateAsync(accTypes);
    } 
    public async Task<AccountTypesDto?> GetAccTypesById(int id)
    {
        var userId = _userService.GetUserId();
        return await _accTypesService.GetAccTypesByIdAsync(id, userId);
    }
    public async Task<bool> DeleteAccTypesById(int id)
    {
        var userId = _userService.GetUserId();
        var accType = await _accTypesService.GetAccTypesByIdAsync(id, userId);
        if (accType is null)
        {
            return false;
        }
        await _accTypesService.DeleteAccTypeAsync(id);
        return true;
    }
    public async Task<bool> OrderListAccTypes(IEnumerable<int> ids) 
    {
        var userId = _userService.GetUserId();
        var accTypes = await _accTypesService.GetListAccountTypesAsync(userId);

        var accTypesIds = accTypes.Select(x => x.Id);

        var IdsNotBelong = accTypesIds.Except(ids).ToList();

        if (IdsNotBelong.Any())
        {
            return false;
        }
        var sortedListAccTypes = ids.Select((value, index) =>
            new AccountTypes() { Id = value, Order = index + 1 }).AsEnumerable();

        await _accTypesService.OrderListAsync(sortedListAccTypes);

        return true;
    }

    
}
