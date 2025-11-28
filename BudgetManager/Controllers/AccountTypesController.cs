using AutoMapper;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Extensions;
using BudgetManager.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;

public class AccountTypesController(IAccountTypesService accTypesService, IValidator<AccountTypesDto> validator, IUserService userService, IMapper mapper): Controller
{
    private readonly IAccountTypesService _accTypesService = accTypesService;
    private readonly IValidator<AccountTypesDto> _validator = validator;
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userService.GetUserId();
        var accountTypes = await _accTypesService.GetAccountTypesAsync(userId);
        var accountTypesVM = _mapper.Map<List<AccountTypesVM>?>(accountTypes);
        return View(accountTypesVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountTypesVM accountTypesVM)
    {
        var accTypesDto = _mapper.Map<AccountTypesDto>(accountTypesVM);
        ValidationResult result = await _validator.ValidateAsync(accTypesDto);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View(accountTypesVM);
        }
        await _accTypesService.Create(accTypesDto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> GetExistAccTypesByName(string name)
    {
        var alreadyExistAccTypes = await _accTypesService.ExistAccTypes(name);

        if (alreadyExistAccTypes)
        {
            return Json($"El nombre {name} ya existe.");
        }
        return Json(true);
    }
    [HttpGet]
    public async Task<ActionResult> Edit(int id)
    {
        var result = await _accTypesService.GetAccTypesById(id);
        if (result is null)
        {
            return RedirectToAction("NoFound", "Home");
        }
        var accTypesVM = _mapper.Map<AccountTypesVM>(result);
        return View(accTypesVM);
    }
    [HttpPost]
    public async Task<ActionResult> Update(AccountTypesVM accountTypesVM)
    {
        var existAccTypes = await _accTypesService.ExistAccTypes(accountTypesVM.Name);
        if (existAccTypes) { return RedirectToAction("NoFound", "Home"); }
        var accTypesDto = _mapper.Map<AccountTypesDto>(accountTypesVM);
        await _accTypesService.Update(accTypesDto);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        var existAccTypeDto = await _accTypesService.GetAccTypesById(id);
        if (existAccTypeDto is null)
        {
            return RedirectToAction("NoFound", "Home");
        }
        var existAccTypeVM = _mapper.Map<AccountTypesVM>(existAccTypeDto);
        return View(existAccTypeVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteAccType(int id)
    {
        await _accTypesService.DeleteAccTypesById(id);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> SortAccTypes([FromBody] int[] ids)
    {
        var sortList = await _accTypesService.OrderListAccTypes(ids);
        if (sortList)
        {
            return Forbid();
        }
        return Ok();
    }
}
