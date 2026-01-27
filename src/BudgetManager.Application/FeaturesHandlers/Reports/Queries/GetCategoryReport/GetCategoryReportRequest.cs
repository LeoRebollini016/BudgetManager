using BudgetManager.Domain.Dtos.Report;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetCategoryReport;

public record GetCategoryReportRequest(Guid UserId, int? AccountId) : IRequest<CategoryReportResultDto>;