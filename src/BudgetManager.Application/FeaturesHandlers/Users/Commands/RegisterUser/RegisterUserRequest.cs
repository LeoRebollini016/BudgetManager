using BudgetManager.Domain.Dtos.User;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Users.Commands.RegisterUser;

public record RegisterUserRequest(string Email, string Password) : IRequest<RegisterUserResultDto>;
