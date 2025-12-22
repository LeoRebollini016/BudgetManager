using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Create;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.DeleteAccTypesById;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.OrderListAccTypes;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Update;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.ExistAccTypes;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypes;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesById;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Extensions;
using BudgetManager.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;

public class AccountTypesController(IMediator mediator, IValidator<AccountTypesDto> validator, IUserService userService, IMapper mapper): Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IValidator<AccountTypesDto> _validator = validator;
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        var request = new GetAccTypesRequest(userId);
        var accountTypes = await _mediator.Send(request, ct);
        var accountTypesVM = _mapper.Map<List<AccountTypesVM>?>(accountTypes);
        return View(accountTypesVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountTypesVM accountTypesVM, CancellationToken ct)
    {
        var accTypesDto = _mapper.Map<AccountTypesDto>(accountTypesVM);
        ValidationResult result = await _validator.ValidateAsync(accTypesDto);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View(accountTypesVM);
        }
        var request = new CreateAccTypesRequest(accTypesDto);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> GetExistAccTypesByName(string name, CancellationToken ct)
    {
        var request = new ExistAccTypesRequest(name);
        var response = await _mediator.Send(request, ct);

        if (response)
        {
            return Json($"El nombre {name} ya existe.");
        }
        return Json(true);
    }
    [HttpGet]
    public async Task<ActionResult> Edit(int id, CancellationToken ct)
    {
        var request = new GetAccTypeByIdRequest(id);
        var response = await _mediator.Send(request, ct);
        if (response is null)
        {
            return RedirectToAction("NoFound", "Home");
        }
        var accTypesVM = _mapper.Map<AccountTypesVM>(response);
        return View(accTypesVM);
    }
    [HttpPost]
    public async Task<ActionResult> Edit(AccountTypesVM accountTypesVM, CancellationToken ct)
    {
        var requestExistAccTypes = new ExistAccTypesRequest(accountTypesVM.Name);
        var response = await _mediator.Send(requestExistAccTypes, ct);
        if (response) { return RedirectToAction("NoFound", "Home"); }
        var accTypesDto = _mapper.Map<AccountTypesDto>(accountTypesVM);

        var request = new UpdateAccTypesRequest(accTypesDto);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var request = new GetAccTypeByIdRequest(id);
        var existAccTypeDto = await _mediator.Send(request, ct);
        if (existAccTypeDto is null)
        {
            return RedirectToAction("NoFound", "Home");
        }
        var existAccTypeVM = _mapper.Map<AccountTypesVM>(existAccTypeDto);
        return View(existAccTypeVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var request = new DeleteAccTypesRequest(id);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> SortAccTypes([FromBody] int[] ids, CancellationToken ct)
    {
        var request = new OrderAccTypesRequest(ids);
        var sortList = await _mediator.Send(request, ct);
        if (sortList)
        {
            return Forbid();
        }
        return Ok();
    }
}
