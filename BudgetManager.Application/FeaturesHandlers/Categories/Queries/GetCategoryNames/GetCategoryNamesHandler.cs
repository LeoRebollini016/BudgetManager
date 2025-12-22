using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryNames;

public class GetCategoryNamesHandler(ICategoryService categoryService) : IRequestHandler<GetCategoryNamesRequest, List<KeyValueDto>>
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<List<KeyValueDto>> Handle(GetCategoryNamesRequest request, CancellationToken cancellationToken)
        => await _categoryService.GetCategoryNamesAsync(cancellationToken);
}