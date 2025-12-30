using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryNames;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.DeleteTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.InsertTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.UpdateTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;
using BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionList;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Extensions;
using BudgetManager.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers;

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
        var transactionListVM = _mapper.Map<List<TransactionDetailsVM>>(transactionListDto);
        return View(transactionListVM);
    }
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var model = await GetTransactionVM(userId, ct);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(TransactionCreateVM transactionCreateVM, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var transactionDto = _mapper.Map<TransactionCreateDto>(transactionCreateVM);
        var request = new InsertTransactionRequest(userId, transactionDto);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var model = await GetTransactionVM(userId, ct);
        model.Id = id;
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(TransactionCreateVM transactionVM, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var transactionDto = _mapper.Map<TransactionCreateDto>(transactionVM);
        var request = new UpdateTransactionRequest(userId, transactionDto);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetTransactionDeleteInfoRequest(userId, id);
        var transactionDeleteInfoDto = await _mediator.Send(request, ct);
        var transactionDeleteInfoVM = _mapper.Map<TransactionDeleteVM>(transactionDeleteInfoDto);
        return View(transactionDeleteInfoVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new DeleteTransactionRequest(userId, id);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    private async Task<TransactionCreateVM> GetTransactionVM(Guid userId, CancellationToken ct)
    {
        var categoryRequest = new GetCategoryNamesRequest(userId);
        var accountRequest = new GetAccountNamesRequest(userId);

        var categoriesOptions = await _mediator.Send(categoryRequest, ct);
        var accountsOptions = await _mediator.Send(accountRequest, ct);
        return new TransactionCreateVM
        {
            Category = categoriesOptions.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }),
            Account = accountsOptions!.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
            }),
            TransactionDate = DateTime.Now
        };
    }
}
