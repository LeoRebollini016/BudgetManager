using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Extensions;
using BudgetManager.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers;

public class AccountController(IAccountService accountService, IUserService userService, IValidator<AccountDto> validator, IMapper mapper) : Controller
{
    private readonly IAccountService _accountService = accountService;
    private readonly IUserService _userService = userService;
    private readonly IValidator<AccountDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountsListDto = await _accountService.GetListAccountsAsync();
        var accountsListVM = _mapper.Map<List<AccountVM>>(accountsListDto);
        return View(accountsListVM);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var modelCreationAccount = await GetAccountsTypes();
        return View(modelCreationAccount);
    }
    [HttpPost]
    public async Task<IActionResult> Create(AccountVM accountVM)
    {
        var id = _userService.GetUserId();
        accountVM.Id = id;
        var accountDto = _mapper.Map<AccountDto>(accountVM);
        ValidationResult result = await _validator.ValidateAsync(accountDto);
        if (!result.IsValid)
        {
            var options = await _accountService.GetAccountTypesNamesAsync();
            var modelCreationAccount = new AccountCreateVM
            {
                AccountTypes = options.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
            };
            result.AddToModelState(this.ModelState);
            return View(modelCreationAccount);
        }

        await _accountService.CreateAsync(accountDto);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var modelCreationAccount = await GetAccountsTypes();
        modelCreationAccount.Id = id;
        return View(modelCreationAccount);
    }
    public async Task<IActionResult> EditAccount(AccountVM accountVM)
    {
        var accountDto = _mapper.Map<AccountDto>(accountVM);
        accountDto.Id = accountVM.Id;
        await _accountService.UpdateAccountAsync(accountDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);
        if (account == null)
        {
            return RedirectToAction("NotFound", "Home");
        }
        var accountVM = _mapper.Map<AccountVM>(account);
        return View(accountVM);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        await _accountService.DeleteAccountAsync(id);
        return RedirectToAction("Index");
    }

    private async Task<AccountCreateVM> GetAccountsTypes()
    {
        var options = await _accountService.GetAccountTypesNamesAsync();
        var modelCreationAccount = new AccountCreateVM
        {
            AccountTypes = options.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
        };
        return modelCreationAccount;
    }
}
