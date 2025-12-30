using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;
using BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetCategoryReport;
using BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetMonthlyReport;
using BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetRangeReport;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Extensions;
using BudgetManager.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers;

[Authorize]
public class ReportController(IMediator mediator, IMapper mapper) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var vm = new ReportViewModel
        {
            Month = DateTime.Now
        };
        var userId = User.GetUserId();
        await LoadAccountsAsync(vm, userId, ct);

        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Monthly(ReportViewModel vm, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var filter = _mapper.Map<MonthlyReportFilterDto>(vm);
        var request = new GetMonthlyReportRequest(userId, filter);
        var result = await _mediator.Send(request, ct);
        vm = _mapper.Map<ReportViewModel>(result);
        await LoadAccountsAsync(vm, userId, ct);
        vm.ReportType = "monthly";
        return View("Index", vm);
    }
    [HttpPost]
    public async Task<IActionResult> Range(ReportViewModel vm, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var filter = _mapper.Map<DateRangeReportFilterDto>(vm);
        var request = new GetRangeReportRequest(userId, filter);
        var result = await _mediator.Send(request, ct);
        vm = _mapper.Map<ReportViewModel>(result);
        vm.ReportType = "range";
        await LoadAccountsAsync(vm, userId, ct);
        return View("Index", vm);
    }
    [HttpPost]
    public async Task<IActionResult> Category(ReportViewModel vm, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetCategoryReportRequest(userId, vm.AccountId);
        var results = await _mediator.Send(request, ct);
        vm = _mapper.Map<ReportViewModel>(results);
        vm.ReportType = "category";
        await LoadAccountsAsync(vm, userId, ct);
        return View("Index", vm);
    }
    private async Task<ReportViewModel> LoadAccountsAsync(ReportViewModel vm, Guid userId, CancellationToken ct)
    {
        var request = new GetAccountNamesRequest(userId);
        var accounts = await _mediator.Send(request, ct);
        vm.SelectOptions = accounts?.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }) ?? [];
        return vm;
    }
}
