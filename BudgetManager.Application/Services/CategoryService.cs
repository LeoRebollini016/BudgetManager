using AutoMapper;
using BudgetManager.Domain.Common;
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
    public async Task<Result> AddCategoryAsync(Category category, CancellationToken ct)
    {
        var exists = await _categoryRepository.ExistsCategoryByNameAsync(category.UserId, category.Name, ct);
        if (exists)
        {
            return Result.Fail($"Ya existe una categoría llamada {category.Name}", nameof(category.Name));
        }
        await _categoryRepository.InsertCategoryAsync(category, ct);
        return Result.Ok();
    }
    public async Task<Result> UpdateCategoryAsync(Category category, CancellationToken ct)
    {
        var existing = await _categoryRepository.GetCategoryByIdAsync(category.UserId, category.Id, ct);
        if (existing is null)
        {
            return Result.Fail("La categoría no existe o no tienes permisos", null);
        }
        var isDuplicate = await _categoryRepository.ExistsCategoryByNameAsync(category.UserId, category.Name, ct, category.Id);
        if(isDuplicate)
        {
            return Result.Fail($"Ya existe una categoría llamada {category.Name}", nameof(category.Name));
        }
        await _categoryRepository.UpdateCategoryAsync(category, ct);
        return Result.Ok();
    }
    public async Task<List<KeyValueDto>> GetCategoryNamesAsync(Guid userId, CancellationToken ct)
        => (List<KeyValueDto>)await _categoryRepository.GetCategoryNamesAsync(userId, ct);
    public async Task<CategoryDeleteDto?> GetCategoryDeleteInfoAsync(Guid userId, int categoryId, CancellationToken ct)
        => await _categoryRepository.GetCategoryDeleteInfoByIdAsync(userId, categoryId, ct);
    public async Task<Result> DeleteCategoryByIdAsync(Guid userId, int categoryId, CancellationToken ct)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(userId, categoryId, ct);
        if (category is null)
        {
            return Result.Fail("La categoría no existe o no tienes permisos", null);
        }
        var hasTransactions = await _categoryRepository.HasTransactionsAsync(userId, categoryId, ct);
        if (hasTransactions)
        {
            return Result.Fail("No se puede eliminar una categoría que tiene transacciones asociadas", string.Empty);
        }
        await _categoryRepository.DeleteCategoryByIdAsync(userId, categoryId, ct);
        return Result.Ok();
    }
}
