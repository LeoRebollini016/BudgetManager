using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.DeleteCategory;

public record DeleteCategoryRequest(int CategoryId) : IRequest<Unit>;
