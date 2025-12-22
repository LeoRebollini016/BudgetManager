using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<ReportTimeSeriesDto>> GetReportByRangeAsync(DateRangeReportFilterDto filter, CancellationToken ct);
    Task<IEnumerable<ReportTimeSeriesDto>> GetReportMonthlyAsync(MonthlyReportFilterDto filter, CancellationToken ct);
    Task<IEnumerable<ReportCategoryDto>> GetReportCategoryAsync(int? AccountId, CancellationToken ct);
}
