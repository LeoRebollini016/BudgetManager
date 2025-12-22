using BudgetManager.Domain.Dtos.Report;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetMonthlyReport;

public record GetMonthlyReportRequest(MonthlyReportFilterDto FilterDto): IRequest<MonthlyReportResultDto>;
