using App.Repositories.Models.Products;
using App.Service.Products;
using App.Service.Products.Create;
using App.Service.Products.Update;
using AutoMapper;

namespace App.Service.Products.AutoMappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap(); //Product to ProductDTO && ProductDTO to Product

        //inputtan gelen name i toLower luyoruz, ve bu bir reverse değil tek taraflı bir mapping
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name!.ToLowerInvariant()));

        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name!.ToLowerInvariant()));



    }
}
