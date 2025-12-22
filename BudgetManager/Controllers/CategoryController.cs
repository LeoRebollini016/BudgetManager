using AutoMapper;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.DeleteCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategories;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryById;
using BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryDeleteInfo;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;

public class CategoryController(IMediator mediator, IMapper mapper) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var request = new GetCategoriesRequest();
        var listCategoriesDto= await _mediator.Send(request, ct);
        var listCategoriesVM = _mapper.Map<List<CategoryVM>>(listCategoriesDto);
        return View(listCategoriesVM);
    }
    [HttpGet]
    public async Task<IActionResult> Detalle(int id, CancellationToken ct)
    {
        var request = new GetCategoryByIdRequest(id);
        var category = await _mediator.Send(request, ct);
        var categoryVM = _mapper.Map<CategoryVM>(category);
        return View(categoryVM);
    }
    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryVM categoryVM, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", categoryVM);
        }
        var categoryDto = _mapper.Map<CategoryDto>(categoryVM);
        var request = new AddCategoryRequest(categoryDto);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var request = new GetCategoryByIdRequest(id);
        var category = await _mediator.Send(request, ct);
        var modelCategory = _mapper.Map<CategoryVM>(category);
        modelCategory.Id = id;
        return View(modelCategory);
    } 

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryVM categoryVM, CancellationToken ct)
    {
        var categoryDto = _mapper.Map<CategoryDto>(categoryVM);
        categoryDto.Id = categoryVM.Id;
        var request = new UpdateCategoryRequest(categoryDto);
        await _mediator.Send(request, ct);

        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var request = new GetCategoryDeleteInfoRequest(id);
        var categoryResumeDto = await _mediator.Send(request, ct);
        var modelDeleteVM = _mapper.Map<CategoryDeleteVM>(categoryResumeDto);
        return View(modelDeleteVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var request = new DeleteCategoryRequest(id);
        await _mediator.Send(request, ct);
        return RedirectToAction("Index");
    }
}
