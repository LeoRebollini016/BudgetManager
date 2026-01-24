using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.DeleteCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategories;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryById;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryDeleteInfo;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Extensions;
using BudgetManager.Models.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;
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
    //[HttpGet]
    //public async Task<IActionResult> Detalle(int id, CancellationToken ct)
    //{
    //    var userId = User.GetUserId();
    //    var request = new GetCategoryByIdRequest(userId, id);
    //    var category = await _mediator.Send(request, ct);
    //    var categoryVM = _mapper.Map<CategoryFormVM>(category);
    //    return View(categoryVM);
    //}
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
        await _mediator.Send(request, ct);
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
        await _mediator.Send(request, ct);

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
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
}
