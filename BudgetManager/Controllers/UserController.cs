using BudgetManager.Application.FeaturesHandlers.Users.Commands.LoginUser;
using BudgetManager.Application.FeaturesHandlers.Users.Commands.RegisterUser;
using BudgetManager.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;

[AllowAnonymous]
public class UserController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
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
    public IActionResult Login() => View();

    [HttpPost]
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
}