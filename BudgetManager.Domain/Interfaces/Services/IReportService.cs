using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IReportService
{
    Task<MonthlyReportResultDto> GetReportMonthlyAsync(Guid userId, MonthlyReportFilterDto filter, CancellationToken ct);
    Task<DateRangeReportResultDto> GetReportRangeAsync(Guid userId, DateRangeReportFilterDto filter, CancellationToken ct);
    Task<CategoryReportResultDto> GetReportCategoryAsync(Guid userId, int? accountId, CancellationToken ct);
}
