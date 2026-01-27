using BudgetManager.Domain.Dtos.User;
using BudgetManager.Infraestructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Application.FeaturesHandlers.Users.Commands.RegisterUser;

public class RegisterUserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IRequestHandler<RegisterUserRequest, RegisterUserResultDto>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<RegisterUserResultDto> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new RegisterUserResultDto
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        await _signInManager.SignInAsync(user, isPersistent: false);

        return new RegisterUserResultDto
        {
            Success = true,
        };
    }
}
