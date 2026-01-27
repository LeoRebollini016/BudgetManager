using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Users.Commands.LogoutUser;

public record LogoutUserRequest() : IRequest<Unit>;
