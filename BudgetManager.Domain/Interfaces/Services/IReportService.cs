using BudgetManager.Domain.Dtos.Report;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IReportService
{
    Task<MonthlyReportResultDto> GetReportMonthlyAsync(MonthlyReportFilterDto monthlyReportFilterDto);
    Task<DateRangeReportResultDto> GetReportRangeAsync(DateRangeReportFilterDto filter);
    Task<CategoryReportResultDto> GetReportCategoryAsync(int? accountId);
}
