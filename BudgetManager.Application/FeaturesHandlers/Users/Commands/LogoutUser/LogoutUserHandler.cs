using BudgetManager.Infraestructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Application.FeaturesHandlers.Users.Commands.LogoutUser;

public class LogoutUserHandler(SignInManager<ApplicationUser> signInManager): IRequestHandler<LogoutUserRequest, Unit>
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    public async Task<Unit> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        return Unit.Value;
    }
}