using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;

public record UpdateCategoryRequest(CategoryDto CategoryDto) : IRequest<Unit>;
