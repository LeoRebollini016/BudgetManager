using AutoMapper;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Controllers
{
    public class ReportController(IReportService reportService, IAccountService accountService, ICategoryService categoryService, IMapper mapper) : Controller
    {
        private readonly IReportService _reportService = reportService;
        private readonly IAccountService _accountService = accountService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var vm = new ReportViewModel
            {
                Month = DateTime.Now
            };
            await LoadAccountsAsync(vm);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Monthly(ReportViewModel vm)
        {
            var filter = _mapper.Map<MonthlyReportFilterDto>(vm);
            var result = await _reportService.GetReportMonthlyAsync(filter);
            vm = _mapper.Map<ReportViewModel>(result);
            await LoadAccountsAsync(vm);
            return View("Index", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Range(ReportViewModel vm)
        {
            var filter = _mapper.Map<DateRangeReportFilterDto>(vm);
            var result = await _reportService.GetReportRangeAsync(filter);
            vm = _mapper.Map<ReportViewModel>(result);
            vm.ReportType = "range";
            await LoadAccountsAsync(vm);
            return View("Index", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Category(ReportViewModel vm)
        {
            var results = await _reportService.GetReportCategoryAsync(vm.AccountId);
            vm = _mapper.Map<ReportViewModel>(results);
            vm.ReportType = "category";
            await LoadAccountsAsync(vm);
            return View("Index", vm);
        }
        private async Task<ReportViewModel> LoadAccountsAsync(ReportViewModel vm)
        {
            var accounts = await _accountService.GetListAccountsAsync();
            vm.SelectOptions = accounts.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            return vm;
        }
    }
}
