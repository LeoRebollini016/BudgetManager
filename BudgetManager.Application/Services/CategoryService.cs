using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, IUserService userService, IMapper mapper) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var user = _userService.GetUserId();
        return (List<CategoryDto>)await _categoryRepository.GetCategoriesAsync(user);
    }
    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var user = _userService.GetUserId();
        var category = await _categoryRepository.GetCategoryByIdAsync(user, id);
        return _mapper.Map<CategoryDto?>(category);
    }
    public async Task AddCategoryAsync(CategoryDto categoryDto)
    {
        var user = _userService.GetUserId();
        categoryDto.userId = user;
        await _categoryRepository.InsertCategoryAsync(
                                        _mapper.Map<Category>(categoryDto));
    }
    public async Task UpdateCategoryAsync(CategoryDto categoryDto)
    {
        var user = _userService.GetUserId();
        categoryDto.userId = user;
        await _categoryRepository.UpdateCategoryAsync(_mapper.Map<Category>(categoryDto));
    }
    public async Task<List<KeyValueDto>> GetCategoryNamesAsync()
    {
        var user = _userService.GetUserId();
        return (List<KeyValueDto>)await _categoryRepository.GetCategoryNamesAsync(user);
    }
}
