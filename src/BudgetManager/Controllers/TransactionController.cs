using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryNames;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.DeleteTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.InsertTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.UpdateTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;
using BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionList;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Web.Extensions;
using BudgetManager.Web.Models;
using BudgetManager.Web.Models.Transaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Web.Controllers;

[Authorize]
public class TransactionController(IMediator mediator, IMapper mapper) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetTransactionListRequest(userId);
        var transactionListDto = await _mediator.Send(request, ct);
        var transactionListVM = _mapper.Map<List<TransactionListVM>>(transactionListDto);
        return View(transactionListVM);
    }
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var model = new TransactionFormVM { TransactionDate = DateTime.Today };
        await LoadTransactionSelects(model, userId, ct);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(TransactionFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            await LoadTransactionSelects(model, userId, ct);
            return View(model);
        }
        var transactionDto = _mapper.Map<TransactionCreateDto>(model);
        var request = new InsertTransactionRequest(userId, transactionDto);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if(result.TargetField is null)
                return RedirectToAction("Error", "Home", new { message = result.Error });

            ModelState.AddModelError(result.TargetField, result.Error ?? "Ha ocurrido un error inesperado.");
            return View(model);
        }
        TempData["SuccessMessage"] = "Transacción creada exitosamente.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetTransactionByIdRequest(userId, id);
        var transaction = await _mediator.Send(request, ct);

        if (transaction is null)
        {
            return RedirectToAction("NotFound", "Home");
        }
        var model = _mapper.Map<TransactionFormVM>(transaction);
        await LoadTransactionSelects(model, userId, ct);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(TransactionFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            await LoadTransactionSelects(model, userId, ct);
            return View(model);
        }
        var transactionDto = _mapper.Map<TransactionCreateDto>(model);
        var request = new UpdateTransactionRequest(userId, transactionDto);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if(result.TargetField is null)
                return RedirectToAction("Error", "Home", new { message = result.Error });

            ModelState.AddModelError(result.TargetField, result.Error ?? "Ha ocurrido un error inesperado.");
            return View(model);
        }
        TempData["SuccessMessage"] = "Transacción actualizada exitosamente.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetTransactionByIdRequest(userId, id);
        var transactionDto = await _mediator.Send(request, ct);

        var transactionDeleteInfoVM = _mapper.Map<TransactionDeleteVM>(transactionDto);
        return View(transactionDeleteInfoVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new DeleteTransactionRequest(userId, id);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if(result.TargetField is null)
                return RedirectToAction("Error", "Home", new { message = result.Error });

            TempData["ErrorMessage"] = result.Error ?? "Ha ocurrido un error inesperado.";
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Transacción eliminada exitosamente.";
        return RedirectToAction("Index");
    }
    private async Task LoadTransactionSelects(TransactionFormVM model, Guid userId, CancellationToken ct)
    {
        var categoryRequest = new GetCategoryNamesRequest(userId);
        var accountRequest = new GetAccountNamesRequest(userId);

        var categoriesOptions = await _mediator.Send(categoryRequest, ct);
        var accountsOptions = await _mediator.Send(accountRequest, ct);
       

        model.Category = categoriesOptions.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name,
        });
        model.Account = accountsOptions!.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name,
        });
    }
}
