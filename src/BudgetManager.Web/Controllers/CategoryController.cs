using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.DeleteCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategories;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryById;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryDeleteInfo;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Web.Extensions;
using BudgetManager.Web.Models.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Web.Controllers;
[Authorize]
public class CategoryController(IMediator mediator, IMapper mapper) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetCategoriesRequest(userId);
        var listCategoriesDto= await _mediator.Send(request, ct);
        var listCategoriesVM = _mapper.Map<List<CategoryListVM>>(listCategoriesDto);
        return View(listCategoriesVM);
    }
    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
            return View(model);
        var categoryDto = _mapper.Map<CategoryDto>(model);
        var request = new AddCategoryRequest(userId, categoryDto);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if(result.TargetField is null) 
            {
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });
            }
            ModelState.AddModelError(result.TargetField, result.Error ?? "Ha ocurrido un error.");
            return View(model);
        }
        TempData["SuccessMessage"] = "Categoría creada exitosamente.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetCategoryByIdRequest(userId, id);
        var category = await _mediator.Send(request, ct);
        var modelCategory = _mapper.Map<CategoryFormVM>(category);
        modelCategory.Id = id;
        return View(modelCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryFormVM model, CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
            return View(model);
        var categoryDto = _mapper.Map<CategoryDto>(model);
        categoryDto.Id = model.Id;
        var request = new UpdateCategoryRequest(userId, categoryDto);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if (result.TargetField is null)
            {
                return RedirectToAction("Error", "Home");
            }
            ModelState.AddModelError(result.TargetField, result.Error ?? "Ha ocurrido un error.");
            return View(model);
        }
        TempData["SuccessMessage"] = "Categoría actualizada exitosamente.";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new GetCategoryDeleteInfoRequest(userId, id);
        var categoryResumeDto = await _mediator.Send(request, ct);
        var modelDeleteVM = _mapper.Map<CategoryDeleteVM>(categoryResumeDto);
        return View(modelDeleteVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var request = new DeleteCategoryRequest(userId, id);
        var result = await _mediator.Send(request, ct);
        if (!result.Success)
        {
            if (result.TargetField is null)
            {
                return RedirectToAction("Error", "Home");
            }
            TempData["ErrorMessage"] = result.Error ?? "Ha ocurrido un error.";
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Categoría eliminada exitosamente.";
        return RedirectToAction("Index");
    }
}
