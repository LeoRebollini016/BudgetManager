using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class ReportRepository(IDbConnectionFactory dbConnection) : IReportRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;
    
    public async Task<IEnumerable<ReportTimeSeriesDto>> GetReportMonthlyAsync(MonthlyReportFilterDto filter)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<ReportTimeSeriesDto>(ReportQueries.GetReportByDateQuery, 
            new
            {
                StartDate = filter.Month,
                NextMonth = filter.Month.AddMonths(1).AddDays(-1),
                filter.AccountId,
            });
    }
    public async Task<IEnumerable<ReportTimeSeriesDto>> GetReportByRangeAsync(DateRangeReportFilterDto filter)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<ReportTimeSeriesDto>(ReportQueries.GetReportByRangeDateQuery, filter);
    }
    public async Task<IEnumerable<ReportCategoryDto>> GetReportCategoryAsync(int? AccountId)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<ReportCategoryDto>(ReportQueries.GetReportByCategoryQuery, new { AccountId });
    }
}
