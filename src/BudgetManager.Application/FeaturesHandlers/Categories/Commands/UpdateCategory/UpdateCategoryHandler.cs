using AutoMapper;
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<UpdateCategoryRequest, Result>
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CategoryDto);
        category.UserId = request.UserId;
        return await _categoryService.UpdateCategoryAsync(category, cancellationToken);
    }
}
