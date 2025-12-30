using BudgetManager.Domain.Dtos.User;
using BudgetManager.Infraestructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Application.FeaturesHandlers.Users.Commands.LoginUser;

public class LoginUserHandler(SignInManager<ApplicationUser> signInManager) : IRequestHandler<LoginUserRequest, LoginUserResultDto>
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<LoginUserResultDto> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(
            request.Email,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: true
        );

        if (!result.Succeeded)
        {
            return new LoginUserResultDto
            {
                Success = false,
                Error = "Email o contraseña inválidos"
            };
        }

        return new LoginUserResultDto
        {
            Success = true
        };
    }
}
