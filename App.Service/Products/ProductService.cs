using App.Repositories.Models.Products;
using App.Repositories.UnitOfWorkPattern;
using App.Service.Products.Create;
using App.Service.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.InteropServices;

namespace App.Service.Products;

public class ProductService(IProductRepository _productRepository, IUnitOfWork _unitOfWork, IMapper mapper) : IProductService
{
    
    public async Task<ServiceResult<CreateProductResponse>> Create(CreateProductRequest createRequest)
    {
        var product = mapper.Map<Product>(createRequest);
        

        await _productRepository.CreateAsync(product);

        await _unitOfWork.SaveChangesAsync();


        var url = $"api/products/{product.Id}";

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), url);

    }


    public async Task<ServiceResult> Delete(int id)
    {
        var item  = await _productRepository.GetByIdAsync(id);

        if(item is null) return ServiceResult.Fail("Item not found");
        
        _productRepository.Delete(item);

        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(statusCode:HttpStatusCode.NoContent);
    }


    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await _productRepository.GetAllListAsync().ToListAsync();

        #region AutoMapper
        var productAsDto = mapper.Map<List<ProductDto>>(products);
        #endregion

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }


    public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if(product is null) return ServiceResult<ProductDto>.Fail("Product not found");

        #region AutoMapper
        var productAsDto = mapper.Map<ProductDto>(product);
        #endregion
        return ServiceResult<ProductDto>.Success(productAsDto);
    }


    public async Task<ServiceResult<ProductDto>> GetByMaxPrice()
    {
        var product = await _productRepository.Where(i=> i.Price == _productRepository.GetAllListAsync().Max(i => i.Price)).ToListAsync();

        #region AutoMapper
        var productAsDto = mapper.Map<ProductDto>(product);
        #endregion

        return ServiceResult<ProductDto>.Success(productAsDto);
    }


    public async Task<ServiceResult<ProductDto>> GetByMinPrice()
    {
        var product = await _productRepository.Where(i => i.Price == _productRepository.GetAllListAsync().Min(i => i.Price)).ToListAsync();

        #region AutoMapper
        var productAsDto = mapper.Map<ProductDto>(product);
        #endregion

        return ServiceResult<ProductDto>.Success(productAsDto);
    }


    public async Task<ServiceResult<List<ProductDto>>> GetPagedWithAl(int pageNumber, int pageSize)
    {
        int startValue = (pageNumber - 1) * pageSize; //(2-1)*10 = 10
        var products = await _productRepository.GetAllListAsync().Skip(startValue).Take(pageNumber).ToListAsync();

        //automapper
        var productAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPriceWithKdv()
    {
        var products = await _productRepository.GetAllListAsync().ToListAsync();

        #region ManuelMapping
        //var productAsDto = products.Select(i => new ProductDTO(i.Id, i.Name, i.UnitStock, i.Price * 1.18m)).ToList();
        #endregion

        //kdv
        products.ForEach(i=> i.Price = i.Price * 1.20m);

        var productAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productAsDto);

    }

    public async Task<ServiceResult> Update(int Id ,UpdateProductRequest updateRequest)
    {
        var product = await _productRepository.GetByIdAsync(Id);

        if (product is null) return ServiceResult.Fail("Product not found");

        product.Name = updateRequest.Name;
        product.Price = updateRequest.Price;
        product.UnitStock = updateRequest.UnitStock;

        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);


    }
}
