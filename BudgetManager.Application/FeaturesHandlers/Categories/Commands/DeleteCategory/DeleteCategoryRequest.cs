using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.DeleteCategory;

public record DeleteCategoryRequest(Guid UserId, int CategoryId) : IRequest<Unit>;
