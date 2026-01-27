using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;
using System.Web.Mvc;

namespace BudgetManager.Infraestructure.Repositories;

public class ReportRepository(IDbConnectionFactory dbConnection) : IReportRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;
    
    public async Task<IEnumerable<ReportTimeSeriesDto>> GetReportMonthlyAsync(Guid userId, MonthlyReportFilterDto filter, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            ReportQueries.GetReportByDateQuery,
            new
            {
                UserId = userId,
                StartDate = filter.Month,
                NextMonth = filter.Month.AddMonths(1).AddDays(-1),
                filter.AccountId,
            },
            cancellationToken: ct
        );
        return await conn.QueryAsync<ReportTimeSeriesDto>(command);
    }
    public async Task<IEnumerable<ReportTimeSeriesDto>> GetReportByRangeAsync(Guid userId, DateRangeReportFilterDto filter, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            ReportQueries.GetReportByRangeDateQuery,
            new
            {
                UserId = userId,
                filter.StartDate,
                filter.EndDate,
                filter.AccountId,
            },
            cancellationToken: ct
        );
        return await conn.QueryAsync<ReportTimeSeriesDto>(command);
    }
    public async Task<IEnumerable<ReportCategoryDto>> GetReportCategoryAsync(Guid userId, int? accountId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            ReportQueries.GetReportByCategoryQuery,
            new
            {
                UserId = userId,
                AccountId = accountId,
            },
            cancellationToken: ct
        );
        return await conn.QueryAsync<ReportCategoryDto>(command);
    }
}