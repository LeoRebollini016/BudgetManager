using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<ReportTimeSeriesDto>> GetReportByRangeAsync(Guid userId, DateRangeReportFilterDto filter, CancellationToken ct);
    Task<IEnumerable<ReportTimeSeriesDto>> GetReportMonthlyAsync(Guid userId, MonthlyReportFilterDto filter, CancellationToken ct);
    Task<IEnumerable<ReportCategoryDto>> GetReportCategoryAsync(Guid userId, int? AccountId, CancellationToken ct);
}
