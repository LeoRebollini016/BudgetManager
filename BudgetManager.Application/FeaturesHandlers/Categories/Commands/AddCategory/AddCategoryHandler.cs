using AutoMapper;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;

public class AddCategoryHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<AddCategoryRequest, Unit>
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    public async Task<Unit> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CategoryDto);
        await _categoryService.AddCategoryAsync(category, cancellationToken);
        return Unit.Value;
    }
}
