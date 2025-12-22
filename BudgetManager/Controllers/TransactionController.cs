using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryNames;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.DeleteTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.InsertTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Commands.UpdateTransaction;
using BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;
using BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionList;
using BudgetManager.Domain.Constants.Enum;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;

namespace BudgetManager.Controllers
{
    public class TransactionController(IMediator mediator, IMapper mapper) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var request = new GetTransactionListRequest();
            var transactionListDto = await _mediator.Send(request, ct);
            var transactionListVM = _mapper.Map<List<TransactionDetailsVM>>(transactionListDto);
            return View(transactionListVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            var model = await GetTransactionVM(ct);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateVM transactionCreateVM, CancellationToken ct)
        {
            var transactionDto = _mapper.Map<TransactionCreateDto>(transactionCreateVM);
            var request = new InsertTransactionRequest(transactionDto);
            await _mediator.Send(request, ct);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var model = await GetTransactionVM(ct);
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TransactionCreateVM transactionVM, CancellationToken ct)
        {
            var transactionDto = _mapper.Map<TransactionCreateDto>(transactionVM);
            var request = new UpdateTransactionRequest(transactionDto);
            await _mediator.Send(request, ct);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var request = new GetTransactionDeleteInfoRequest(id);
            var transactionDeleteInfoDto = await _mediator.Send(request, ct);
            var transactionDeleteInfoVM = _mapper.Map<TransactionDeleteVM>(transactionDeleteInfoDto);
            return View(transactionDeleteInfoVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var request = new DeleteTransactionRequest(id);
            await _mediator.Send(request, ct);
            return RedirectToAction("Index");
        }
        private async Task<TransactionCreateVM> GetTransactionVM(CancellationToken ct)
        {
            var categoryRequest = new GetCategoryNamesRequest();
            var categoriesOptions = await _mediator.Send(categoryRequest, ct);
            var accountRequest = new GetAccountNamesRequest();
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
}
