using BudgetManager.Domain.Dtos.Report;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetMonthlyReport;

public record GetMonthlyReportRequest(Guid UserId, MonthlyReportFilterDto FilterDto): IRequest<MonthlyReportResultDto>;
