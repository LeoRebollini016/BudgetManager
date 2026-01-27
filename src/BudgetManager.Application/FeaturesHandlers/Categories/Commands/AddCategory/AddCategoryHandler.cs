using AutoMapper;
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;

public class AddCategoryHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<AddCategoryRequest, Result>
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CategoryDto);
        category.UserId = request.UserId;
        return await _categoryService.AddCategoryAsync(category, cancellationToken);
    }
}
