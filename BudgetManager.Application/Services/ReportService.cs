using BudgetManager.Domain.Constants.Enum;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class ReportService(IReportRepository reportRepository) : IReportService
{
    private readonly IReportRepository _reportRepository = reportRepository;
    
    public async Task<MonthlyReportResultDto> GetReportMonthlyAsync(MonthlyReportFilterDto monthlyReportFilterDto, CancellationToken ct)
    {
        var items = (List<ReportTimeSeriesDto>)await _reportRepository.GetReportMonthlyAsync(monthlyReportFilterDto, ct);

        return new MonthlyReportResultDto
        {
            TimeSeries = items,
            TotalIncome = items.Sum(x => x.Income),
            TotalExpense = items.Sum(x => x.Expense)
        };
    }
    public async Task<DateRangeReportResultDto> GetReportRangeAsync(DateRangeReportFilterDto filter, CancellationToken ct)
    {
        var items = (List<ReportTimeSeriesDto>)await _reportRepository.GetReportByRangeAsync(filter, ct);

        return new DateRangeReportResultDto
        {
            TimeSeries = items,
            TotalIncome = items.Sum(x => x.Income),
            TotalExpense = items.Sum(x => x.Expense)
        };
    }
    public async Task<CategoryReportResultDto> GetReportCategoryAsync(int? accountId, CancellationToken ct)
    {
        var items = (List<ReportCategoryDto>)await _reportRepository.GetReportCategoryAsync(accountId, ct);

        return new CategoryReportResultDto
        {
            ByCategory = items,
            TotalIncome = items.Where(x => x.OperationType == OperationTypeEnum.Ingreso).Sum(x => x.Total),
            TotalExpense = items.Where(x => x.OperationType == OperationTypeEnum.Gastos).Sum(x => x.Total)
        };
    }

}
