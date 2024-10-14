using App.Repositories.Categories;
using App.Service.Categories.CategoryForDtos;
using App.Service.Categories.Create;
using AutoMapper;
using App.Repositories.Models.Products;
using App.Service.Products;
using App.Service.Categories.CategoryDtoModels;

namespace App.Service.Categories.AutoMappers;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {

        // CreateCategoryRequest -> Category eşlemesi
        CreateMap<CreateCategoryRequest, Category>();

        // Category -> CategoryDto eşlemesi
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Category, CategoryWithProductDto>().ReverseMap();


    }
}