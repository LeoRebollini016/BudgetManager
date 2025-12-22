using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, IUserService userService) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUserService _userService = userService;

    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        var user = _userService.GetUserId();
        return (List<CategoryDto>)await _categoryRepository.GetCategoriesAsync(user, ct);
    }
    public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken ct)
    {
        var user = _userService.GetUserId();
        return await _categoryRepository.GetCategoryByIdAsync(user, id, ct);
    }
    public async Task AddCategoryAsync(Category category, CancellationToken ct)
    {
        var user = _userService.GetUserId();
        category.UserId = user;
        await _categoryRepository.InsertCategoryAsync(category, ct);
    }
    public async Task UpdateCategoryAsync(Category category, CancellationToken ct)
    {
        var user = _userService.GetUserId();
        category.UserId = user;
        await _categoryRepository.UpdateCategoryAsync(category, ct);
    }
    public async Task<List<KeyValueDto>> GetCategoryNamesAsync(CancellationToken ct)
    {
        var user = _userService.GetUserId();
        return (List<KeyValueDto>)await _categoryRepository.GetCategoryNamesAsync(user, ct);
    }
    public async Task<CategoryDeleteDto?> GetCategoryDeleteInfoAsync(int categoryId, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        return await _categoryRepository.GetCategoryDeleteInfoByIdAsync(id: categoryId, userId, ct);
    }
    public async Task DeleteCategoryByIdAsync(int categoryId, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        await _categoryRepository.DeleteCategoryByIdAsync(userId, categoryId, ct);
    }
}
