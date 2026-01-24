using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Commands.CreateAccount;
using BudgetManager.Application.FeaturesHandlers.Accounts.Commands.DeleteAccount;
using BudgetManager.Application.FeaturesHandlers.Accounts.Commands.UpdateAccount;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountById;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccounts;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesNames;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Extensions;
using BudgetManager.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers;

[Authorize]
public class AccountController(IMediator mediator, IMapper mapper) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetAccountsRequest(userId);
        var accountsListDto = await _mediator.Send(request, ct);
        var accountsListVM = _mapper.Map<List<AccountListVM>>(accountsListDto);
        return View(accountsListVM);
    }
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var modelCreationAccount = await GetAccountsTypes(userId, ct);
        return View(modelCreationAccount);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm]AccountFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();

        if(!ModelState.IsValid)
        {
            model.AccountTypes = (await GetAccountsTypes(userId, ct)).AccountTypes;
            return View(model);
        }

        var accountDto = _mapper.Map<AccountDto>(model);
        var request = new CreateAccountRequest(userId, accountDto);
        await _mediator.Send(request);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();

        var request = new GetAccountByIdRequest(userId, id);
        var account = await _mediator.Send(request, ct);

        if (account is null)
            return RedirectToAction("NotFound", "Home");

        var model = _mapper.Map<AccountFormVM>(account);
        model.Id = id;
        model.AccountTypes = (await GetAccountsTypes(userId, ct)).AccountTypes;

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(AccountFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();

        if (!ModelState.IsValid)
        {
            model.AccountTypes = (await GetAccountsTypes(userId, ct)).AccountTypes;
            return View(model);
        }

        var accountDto = _mapper.Map<AccountDto>(model);
        accountDto.Id = model.Id;
        var request = new UpdateAccountRequest(userId, accountDto);
        await _mediator.Send(request, ct);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();

        var request = new GetAccountByIdRequest(userId, id);
        var account = await _mediator.Send(request, ct);

        if (account == null)
        {
            return RedirectToAction("NotFound", "Home");
        }
        var vm = _mapper.Map<AccountDeleteVM>(account);

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new DeleteAccountRequest(userId, id);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }

    private async Task<AccountFormVM> GetAccountsTypes(Guid userId, CancellationToken ct)
    {
        var request = new GetAccTypesNamesRequest(userId);
        var options = await _mediator.Send(request, ct);
        return new AccountFormVM
        {
            AccountTypes = options!.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
        };
    }
}
