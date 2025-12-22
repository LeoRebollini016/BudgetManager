using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategories;

public class GetCategoriesHandler(ICategoryService categoryService) : IRequestHandler<GetCategoriesRequest, List<CategoryDto>>
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<List<CategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        => await _categoryService.GetCategoriesAsync(cancellationToken);
}
