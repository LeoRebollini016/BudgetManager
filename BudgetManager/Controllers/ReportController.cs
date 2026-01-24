using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;
using BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetCategoryReport;
using BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetMonthlyReport;
using BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetRangeReport;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Extensions;
using BudgetManager.Models.Report;
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
    public async Task<IActionResult> Monthly(ReportViewModel model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            await LoadAccountsAsync(model, userId, ct);
            return View(model);
        }
        var filter = _mapper.Map<MonthlyReportFilterDto>(model);
        var request = new GetMonthlyReportRequest(userId, filter);
        var result = await _mediator.Send(request, ct);
        model = _mapper.Map<ReportViewModel>(result);
        await LoadAccountsAsync(model, userId, ct);
        model.ReportType = "monthly";
        return View("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> Range(ReportViewModel model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            await LoadAccountsAsync(model, userId, ct);
            return View(model);
        }
        var filter = _mapper.Map<DateRangeReportFilterDto>(model);
        var request = new GetRangeReportRequest(userId, filter);
        var result = await _mediator.Send(request, ct);
        model = _mapper.Map<ReportViewModel>(result);
        model.ReportType = "range";
        await LoadAccountsAsync(model, userId, ct);
        return View("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> Category(ReportViewModel model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            await LoadAccountsAsync(model, userId, ct);
            return View(model);
        }
        var request = new GetCategoryReportRequest(userId, model.AccountId);
        var results = await _mediator.Send(request, ct);
        model = _mapper.Map<ReportViewModel>(results);
        model.ReportType = "category";
        await LoadAccountsAsync(model, userId, ct);
        return View("Index", model);
    }
    private async Task LoadAccountsAsync(ReportViewModel model, Guid userId, CancellationToken ct)
    {
        var request = new GetAccountNamesRequest(userId);
        var accounts = await _mediator.Send(request, ct);
        model.SelectOptions = accounts?.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }) ?? [];
    }
}
