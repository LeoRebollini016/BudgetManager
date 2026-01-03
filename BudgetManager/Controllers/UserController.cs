using BudgetManager.Application.FeaturesHandlers.Users.Commands.LoginUser;
using BudgetManager.Application.FeaturesHandlers.Users.Commands.LogoutUser;
using BudgetManager.Application.FeaturesHandlers.Users.Commands.RegisterUser;
using BudgetManager.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;

public class UserController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserRegisterVM registerVM, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(registerVM);

        var request = new RegisterUserRequest(registerVM.Email, registerVM.Password);
        var result = await _mediator.Send(request, ct);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View(registerVM);
        }
        return RedirectToAction("Index", "Transaction");
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if(User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Transaction");

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginVM loginVM, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        var request = new LoginUserRequest(loginVM.Email, loginVM.Password);
        var result = await _mediator.Send(request, ct);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(loginVM);
        }
        return RedirectToAction("Index", "Transaction");
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        var request = new LogoutUserRequest();
        await _mediator.Send(request, ct);
        return RedirectToAction("Login", "User");
    }
}