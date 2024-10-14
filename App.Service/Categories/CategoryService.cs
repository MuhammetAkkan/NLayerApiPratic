using System.Net;
using App.Repositories.Categories;
using App.Repositories.UnitOfWorkPattern;
using App.Service.Categories.CategoryForDtos;
using App.Service.Categories.Create;
using App.Service.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using App.Service.Categories.CategoryDtoModels;

namespace App.Service.Categories;

public class CategoryService(ICategoryRepository _categoryRepository, IUnitOfWork _unitOfWork, IMapper mapper) : ICategoryService
{

   

    public async Task<ServiceResult<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllListAsync().ToListAsync();


        if(categories is null)
            return ServiceResult<List<CategoryDto>>.Fail("Categories not found", HttpStatusCode.NotFound);


        var data = mapper.Map<List<CategoryDto>>(categories);

        return ServiceResult<List<CategoryDto>>.Success(data, status:HttpStatusCode.OK);

    }



    public async Task<ServiceResult<List<CategoryWithProductDto>>> GetCategoriesWithProducts()
    {
        var categories = await _categoryRepository.GetCategoriesWithProducts().ToListAsync();
        

        var categoryWithProducts = mapper.Map<List<CategoryWithProductDto>>(categories);


        if (categoryWithProducts is null || categoryWithProducts.Count == 0)
            return ServiceResult<List<CategoryWithProductDto>>.Fail("Mapping is fail", statusCode: HttpStatusCode.NotFound);

        List<CategoryWithProductDto> incele = categoryWithProducts;



        return ServiceResult<List<CategoryWithProductDto>>.Success(categoryWithProducts);
    }



    public async Task<ServiceResult<CategoryWithProductDto>> GetCategoriesWithProducts(int id)
    {
       var items = await _categoryRepository.GetCategoriesWithProductsAsync(id);

        if (items is null)
            return ServiceResult<CategoryWithProductDto>.Fail("Categories not found", HttpStatusCode.NotFound);

        var data = mapper.Map<CategoryWithProductDto>(items);

        return ServiceResult<CategoryWithProductDto>.Success(data, status: HttpStatusCode.OK);
    }



    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var item = await _categoryRepository.GetByIdAsync(id) ?? null;

        if (item is null)
            return ServiceResult<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);

        var data = mapper.Map<CategoryDto>(item);

        return ServiceResult<CategoryDto>.Success(data, status: HttpStatusCode.OK);

    }


    public async Task<ServiceResult<int>> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest)
    {

        //doğrudan dto geliyor ama nesne oluşumunu entity üzerinden yaptığımızdan gelir gelmez mapledik.
        var createdCategory = mapper.Map<Category>(createCategoryRequest);

        await _categoryRepository.CreateAsync(createdCategory);

        await _unitOfWork.SaveChangesAsync();

        var data = mapper.Map<CategoryDto>(createdCategory);

        string url = $"api/categories/{data.id}";

        return ServiceResult<int>.SuccessAsCreated(createdCategory.Id ,url);

    }



    public async Task<ServiceResult> UpdateCategoryAsync(int id, UpdateCategoryRequest updateRequest)
    {
        var item = await _categoryRepository.GetByIdAsync(id) ?? null;

        if(item is null)
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);

        var category = mapper.Map(updateRequest, item);

        _categoryRepository.Update(category);

        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);

    }

    public async Task<ServiceResult> DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);

        _categoryRepository.Delete(category);

        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    

    
}
