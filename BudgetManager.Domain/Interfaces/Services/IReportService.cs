using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IReportService
{
    Task<MonthlyReportResultDto> GetReportMonthlyAsync(MonthlyReportFilterDto monthlyReportFilterDto, CancellationToken ct);
    Task<DateRangeReportResultDto> GetReportRangeAsync(DateRangeReportFilterDto filter, CancellationToken ct);
    Task<CategoryReportResultDto> GetReportCategoryAsync(int? accountId, CancellationToken ct);
}
