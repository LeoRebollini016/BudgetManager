using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<List<CategoryDto>> GetCategoriesAsync(Guid userId, CancellationToken ct)
        => (List<CategoryDto>)await _categoryRepository.GetCategoriesAsync(userId, ct);
    public async Task<Category?> GetCategoryByIdAsync(Guid userId, int id, CancellationToken ct)
        => await _categoryRepository.GetCategoryByIdAsync(userId, id, ct);
    public async Task AddCategoryAsync(Category category, CancellationToken ct)
        => await _categoryRepository.InsertCategoryAsync(category, ct);
    public async Task UpdateCategoryAsync(Category category, CancellationToken ct)
        => await _categoryRepository.UpdateCategoryAsync(category, ct);
    public async Task<List<KeyValueDto>> GetCategoryNamesAsync(Guid userId, CancellationToken ct)
        => (List<KeyValueDto>)await _categoryRepository.GetCategoryNamesAsync(userId, ct);
    public async Task<CategoryDeleteDto?> GetCategoryDeleteInfoAsync(Guid userId, int categoryId, CancellationToken ct)
        => await _categoryRepository.GetCategoryDeleteInfoByIdAsync(userId, categoryId, ct);
    public async Task DeleteCategoryByIdAsync(Guid userId, int categoryId, CancellationToken ct)
        => await _categoryRepository.DeleteCategoryByIdAsync(userId, categoryId, ct);
}
