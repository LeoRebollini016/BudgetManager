using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetCategoryReport;

public class GetCategoryReportHandler(IReportService reportService) : IRequestHandler<GetCategoryReportRequest, CategoryReportResultDto>
{
    private readonly IReportService _reportService = reportService;

    public async Task<CategoryReportResultDto> Handle(GetCategoryReportRequest request, CancellationToken cancellationToken)
        => await _reportService.GetReportCategoryAsync(request.AccountId, cancellationToken);
}
