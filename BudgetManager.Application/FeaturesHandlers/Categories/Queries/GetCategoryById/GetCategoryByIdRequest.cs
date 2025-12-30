using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryById;

public record GetCategoryByIdRequest(Guid UserId, int Id) : IRequest<CategoryDto?>;
