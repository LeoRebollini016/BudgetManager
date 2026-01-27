using AutoMapper;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryById;

public class GetCategoryByIdHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<GetCategoryByIdRequest, CategoryDto?>
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    public async Task<CategoryDto?> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.UserId, request.Id, cancellationToken);
        return _mapper.Map<CategoryDto?>(category);
    }
}