using AutoMapper;
using BudgetManager.Domain.Constants.Enum;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;

namespace BudgetManager.Controllers
{
    public class TransactionController(ITransactionService transactionService, ICategoryService categoryService, IAccountService accountService, IMapper mapper) : Controller
    {
        private readonly ITransactionService _transactionService = transactionService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IAccountService _accountService = accountService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var transactionListDto = await _transactionService.GetTransactionListAsync();
            var transactionListVM = _mapper.Map<List<TransactionDetailsVM>>(transactionListDto);
            return View(transactionListVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await GetTransactionVM();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateVM transactionCreateVM)
        {
            var transactionDto = _mapper.Map<TransactionCreateDto>(transactionCreateVM);
            await _transactionService.InsertTransactionAsync(transactionDto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await GetTransactionVM();
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TransactionCreateVM transactionVM)
        {
            var transactionDto = _mapper.Map<TransactionCreateDto>(transactionVM);
            await _transactionService.UpdateTransactionAsync(transactionDto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transactionDeleteInfoDto = await _transactionService.GetTransactionDeleteInfoByIdAsync(id);
            var transactionDeleteInfoVM = _mapper.Map<TransactionDeleteVM>(transactionDeleteInfoDto);
            return View(transactionDeleteInfoVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransactionByIdAsync(id);
            return RedirectToAction("Index");
        }
        private async Task<TransactionCreateVM> GetTransactionVM()
        {
            var categoriesOptions = await _categoryService.GetCategoryNamesAsync();
            var accountsOptions = await _accountService.GetListAccountsAsync();
            var transactionCreateVM = new TransactionCreateVM
            {
                Category = categoriesOptions.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }),
                Account = accountsOptions.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name,
                }),
                TransactionDate = DateTime.Now
            };
            return transactionCreateVM;
        }
    }
}
