using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Create;
using BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Delete;
using BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Update;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountById;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccounts;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;
using BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesNames;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Extensions;
using BudgetManager.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers;

public class AccountController(IUserService userService, IMediator mediator, IValidator<AccountDto> validator, IMapper mapper) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IMediator _mediator = mediator;
    private readonly IValidator<AccountDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var request = new GetAccountsRequest();
        var accountsListDto = await _mediator.Send(request, ct);
        var accountsListVM = _mapper.Map<List<AccountVM>>(accountsListDto);
        return View(accountsListVM);
    }
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var modelCreationAccount = await GetAccountsTypes(ct);
        return View(modelCreationAccount);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm]AccountVM accountVM, CancellationToken ct)
    {
        var id = _userService.GetUserId();
        accountVM.Id = id;
        var accountDto = _mapper.Map<AccountDto>(accountVM);
        ValidationResult result = await _validator.ValidateAsync(accountDto);
        if (!result.IsValid)
        {
            var requestAccTypesNames = new GetAccTypesNamesRequest();
            var options = await _mediator.Send(requestAccTypesNames, ct);
            var modelCreationAccount = new AccountCreateVM
            {
                AccountTypes = options!.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
            };
            result.AddToModelState(this.ModelState);
            return View(modelCreationAccount);
        }
        var request = new CreateAccountRequest(accountDto);
        await _mediator.Send(request);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var modelCreationAccount = await GetAccountsTypes(ct);
        modelCreationAccount.Id = id;
        return View(modelCreationAccount);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(AccountVM accountVM, CancellationToken ct)
    {
        var accountDto = _mapper.Map<AccountDto>(accountVM);
        accountDto.Id = accountVM.Id;
        var request = new UpdateAccountRequest(accountDto);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var request = new GetAccountByIdRequest(id);
        var account = await _mediator.Send(request, ct);
        if (account == null)
        {
            return RedirectToAction("NotFound", "Home");
        }
        var accountVM = _mapper.Map<AccountVM>(account);
        return View(accountVM);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var request = new DeleteAccountRequest(id);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }

    private async Task<AccountCreateVM> GetAccountsTypes(CancellationToken ct)
    {
        var request = new GetAccTypesNamesRequest();
        var options = await _mediator.Send(request, ct);
        return new AccountCreateVM
        {
            AccountTypes = options!.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
        };
    }
}
