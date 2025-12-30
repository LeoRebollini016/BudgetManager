using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategories;

public record GetCategoriesRequest(Guid UserId) : IRequest<List<CategoryDto>>;