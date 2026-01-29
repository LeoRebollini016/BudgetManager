using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Create;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.DeleteAccTypesById;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.OrderListAccTypes;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Update;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypes;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesById;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Web.Extensions;
using BudgetManager.Web.Models.AccountTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Web.Controllers;

[Authorize]
public class AccountTypesController(IMediator mediator, IMapper mapper): Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetAccTypesRequest(userId);
        var accountTypes = await _mediator.Send(request, ct);
        var accountTypesVM = _mapper.Map<List<AccountTypesListVM>?>(accountTypes);
        return View(accountTypesVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountTypesFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
            return View(model);
        var accTypesDto = _mapper.Map<AccountTypesDto>(model);
        var request = new CreateAccTypesRequest(userId, accTypesDto);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if(result.TargetField is null) 
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });

            ModelState.AddModelError(result.TargetField, result.Error!);
            return View(model);
        }
        TempData["SuccessMessage"] = "Tipo de cuenta creado con éxito.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<ActionResult> Edit(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetAccTypeByIdRequest(userId, id);
        var response = await _mediator.Send(request, ct);
        if (response is null)
        {
            return RedirectToAction("NotFound", "Home");
        }
        var accTypesVM = _mapper.Map<AccountTypesFormVM>(response);
        return View(accTypesVM);
    }
    [HttpPost]
    public async Task<ActionResult> Edit(AccountTypesFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
            return View(model);

        var accTypesDto = _mapper.Map<AccountTypesDto>(model);

        var request = new UpdateAccTypesRequest(userId, accTypesDto);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if(result.TargetField is null)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });
            
            ModelState.AddModelError(result.TargetField, result.Error!);
            return View(model);
        }
        TempData["SuccessMessage"] = "Tipo de cuenta actualizado con éxito.";
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetAccTypeByIdRequest(userId, id);
        var existAccTypeDto = await _mediator.Send(request, ct);
        if (existAccTypeDto is null)
        {
            return RedirectToAction("NotFound", "Home");
        }
        var existAccTypeVM = _mapper.Map<AccountTypesDeleteVM>(existAccTypeDto);
        return View(existAccTypeVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new DeleteAccTypesRequest(userId, id);

        var result = await _mediator.Send(request, ct);
        if(!result.Success)
        {
            if(result.TargetField is null)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });
            TempData["ErrorMessage"] = result.Error;
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Tipo de cuenta eliminado con éxito.";
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> SortAccTypes([FromBody] int[] ids, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new OrderAccTypesRequest(userId, ids);
        var sortList = await _mediator.Send(request, ct);
        if (sortList)
        {
            return Forbid();
        }
        return Ok();
    }
}
