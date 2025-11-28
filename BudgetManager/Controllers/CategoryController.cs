using AutoMapper;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Controllers;

public class CategoryController(ICategoryService categoryService, IMapper mapper) : Controller
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var listCategoriesDto= await _categoryService.GetCategoriesAsync();
        var listCategoriesVM = _mapper.Map<List<CategoryVM>>(listCategoriesDto);
        return View(listCategoriesVM);
    }
    [HttpGet]
    public async Task<IActionResult> Detalle(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        var categoryVM = _mapper.Map<CategoryVM>(category);
        return View(categoryVM);
    }
    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CategoryVM categoryVM)
    {
        var categoryDto = _mapper.Map<CategoryDto>(categoryVM);
        await _categoryService.AddCategoryAsync(categoryDto);

        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id) 
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        var modelCategory = _mapper.Map<CategoryVM>(category);
        modelCategory.Id = id;
        return View(modelCategory);
    } 

    [HttpPost]
    public async Task<IActionResult> UpdateCategory(CategoryVM categoryVM)
    {
        var categoryDto = _mapper.Map<CategoryDto>(categoryVM);
        categoryDto.Id = categoryVM.Id;
        await _categoryService.UpdateCategoryAsync(categoryDto);

        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var categoryResumeDto = await _categoryService.GetCategoryDeleteInfoAsync(id);
        var modelDeleteVM = _mapper.Map<CategoryDeleteVM>(categoryResumeDto);
        return View(modelDeleteVM);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteCategoryByIdAsync(id);
        return RedirectToAction("Index");
    }

}
