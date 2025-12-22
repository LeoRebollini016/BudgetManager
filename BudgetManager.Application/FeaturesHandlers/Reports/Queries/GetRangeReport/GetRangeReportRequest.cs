using BudgetManager.Domain.Dtos.Report;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetRangeReport;

public record GetRangeReportRequest(DateRangeReportFilterDto FilterDto) : IRequest<DateRangeReportResultDto>;