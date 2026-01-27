using BudgetManager.Domain.Dtos.User;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Users.Commands.LoginUser;

public record LoginUserRequest(string Email, string Password) : IRequest<LoginUserResultDto>;