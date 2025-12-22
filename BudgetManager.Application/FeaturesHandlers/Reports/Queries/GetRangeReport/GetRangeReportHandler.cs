using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetRangeReport;

public class GetRangeReportHandler(IReportService reportService) : IRequestHandler<GetRangeReportRequest, DateRangeReportResultDto>
{
    private readonly IReportService _reportService = reportService;

    public async Task<DateRangeReportResultDto> Handle(GetRangeReportRequest request, CancellationToken cancellationToken)
        => await _reportService.GetReportRangeAsync(request.FilterDto, cancellationToken);
}
