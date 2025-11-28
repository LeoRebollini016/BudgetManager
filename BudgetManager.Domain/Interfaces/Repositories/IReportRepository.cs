using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<ReportTimeSeriesDto>> GetReportByRangeAsync(DateRangeReportFilterDto filter);
    Task<IEnumerable<ReportTimeSeriesDto>> GetReportMonthlyAsync(MonthlyReportFilterDto filter);
    Task<IEnumerable<ReportCategoryDto>> GetReportCategoryAsync(int? AccountId);
}
