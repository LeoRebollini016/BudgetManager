using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Reports.Queries.GetMonthlyReport;

public class GetMonthlyReportHandler(IReportService reportService) : IRequestHandler<GetMonthlyReportRequest, MonthlyReportResultDto>
{
    private readonly IReportService _reportService = reportService;

    public async Task<MonthlyReportResultDto> Handle(GetMonthlyReportRequest request, CancellationToken cancellationToken)
        => await _reportService.GetReportMonthlyAsync(request.FilterDto, cancellationToken);
}
