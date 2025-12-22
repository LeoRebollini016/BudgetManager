using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategories;

public class GetCategoriesRequest : IRequest<List<CategoryDto>>;