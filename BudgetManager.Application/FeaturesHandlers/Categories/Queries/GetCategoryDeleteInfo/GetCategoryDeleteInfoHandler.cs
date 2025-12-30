using AutoMapper;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryDeleteInfo;

public class GetCategoryDeleteInfoHandler(ICategoryService categoryService) : IRequestHandler<GetCategoryDeleteInfoRequest, CategoryDeleteDto?>
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<CategoryDeleteDto?> Handle(GetCategoryDeleteInfoRequest request, CancellationToken cancellationToken)
        => await _categoryService.GetCategoryDeleteInfoAsync(request.UserId, request.CategoryId, cancellationToken);
}