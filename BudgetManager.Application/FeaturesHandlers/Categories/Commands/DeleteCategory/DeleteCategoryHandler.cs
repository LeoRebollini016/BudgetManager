using BudgetManager.Domain.Common;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler(ICategoryService categoryService) : IRequestHandler<DeleteCategoryRequest, Result>
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<Result> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        => await _categoryService.DeleteCategoryByIdAsync(request.UserId, request.CategoryId, cancellationToken);
}