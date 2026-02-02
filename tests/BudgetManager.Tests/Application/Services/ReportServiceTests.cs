using BudgetManager.Application.Services;
using BudgetManager.Domain.Constants.Enum;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace BudgetManager.Tests.Application.Services;

public class ReportServiceTests
{
    private readonly ReportService _service;
    private readonly Mock<IReportRepository> _reportRepoMock;
    private readonly Guid _userId = Guid.NewGuid();

    public ReportServiceTests()
    {
        _reportRepoMock = new Mock<IReportRepository>();
        _service = new ReportService(_reportRepoMock.Object);
    }

    [Theory]
    [InlineData(1, 0, false)]
    [InlineData(-367, 0, false)]
    [InlineData(-30, 0, true)]
    public async Task GetReportRange_ValidateDateRange_Behavior(int startDate, int endDate, bool expectedSuccess)
    {
        // Arrange
        var filter = new DateRangeReportFilterDto
        {
            StartDate = DateTime.Now.AddDays(startDate),
            EndDate = DateTime.Now.AddDays(endDate)
        };
        if(expectedSuccess)
        {
            _reportRepoMock.Setup(x => x.GetReportByRangeAsync(It.IsAny<Guid>(),
                    It.IsAny<DateRangeReportFilterDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ReportTimeSeriesDto>());
        }
        // Act
        var result = await _service.GetReportRangeAsync(_userId, filter, CancellationToken.None);

        // Assert
        if (expectedSuccess)
        {
            result.Should().NotBeNull();
            _reportRepoMock.Verify(x => x.GetReportByRangeAsync(It.IsAny<Guid>(), It.IsAny<DateRangeReportFilterDto>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
        else
        {
            result.Should().BeNull();
            _reportRepoMock.Verify(x => x.GetReportByRangeAsync(It.IsAny<Guid>(), It.IsAny<DateRangeReportFilterDto>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }
    }
    [Theory]
    [InlineData(350, 250, 350, 250)]
    [InlineData(0, 500, 0, 500)]
    public async Task GetReportCategory_CalculateTotals_BasedOnOperationTypes(decimal amount1, decimal amount2,
        decimal expectedIncome, decimal expectedExpense)
    {
        // Arrange
        var mockItems = new List<ReportCategoryDto>
        {
            new() { CategoryName = "Categoría1", Total = amount1, OperationType = OperationTypeEnum.Ingreso },
            new() { CategoryName = "Categoría2", Total = amount2, OperationType = OperationTypeEnum.Gastos }
        };

        _reportRepoMock.Setup(x => x.GetReportCategoryAsync(It.IsAny<Guid>(),
                It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockItems);
        // Act
        var result = await _service.GetReportCategoryAsync(_userId, null, CancellationToken.None);

        // Assert
        result.TotalIncome.Should().Be(expectedIncome);
        result.TotalExpense.Should().Be(expectedExpense);
        result.ByCategory.Should().HaveCount(2);
    }
}
