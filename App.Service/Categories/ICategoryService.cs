using App.Service.Categories.CategoryDtoModels;
using App.Service.Categories.CategoryForDtos;
using App.Service.Categories.Create;
using App.Service.Categories.Update;

namespace App.Service.Categories;

public interface ICategoryService
{
    Task<ServiceResult<List<CategoryDto>>> GetAllCategoriesAsync();

    Task<ServiceResult<List<CategoryWithProductDto>>> GetCategoriesWithProducts();
    Task<ServiceResult<CategoryWithProductDto>> GetCategoriesWithProducts(int id);

    Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);

    Task<ServiceResult<int>> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);

    Task<ServiceResult> UpdateCategoryAsync(int id, UpdateCategoryRequest updateRequest);

    Task<ServiceResult> DeleteCategoryAsync(int id);
}