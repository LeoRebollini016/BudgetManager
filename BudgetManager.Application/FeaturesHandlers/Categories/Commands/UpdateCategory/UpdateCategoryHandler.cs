using AutoMapper;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<UpdateCategoryRequest, Unit>
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    public async Task<Unit> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.CategoryDto);
        await _categoryService.UpdateCategoryAsync(category, cancellationToken);
        return Unit.Value;
    }
}
