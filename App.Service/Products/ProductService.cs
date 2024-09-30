using App.Repositories.Models.Products;
using App.Repositories.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            UnitStock = request.UnitStock
        };

        await productRepository.CreateAsync(product);

        await unitOfWork.SaveChangesAsync();

        var url = $"api/products/{product.Id}";

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), url);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var item = await productRepository.GetByIdAsync(id);

        if(item is null)
        {
            return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.BadRequest);
        }

        productRepository.Delete(item);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var items = productRepository.GetAllListAsync();

        if(!items.Any())
        {
            return ServiceResult<List<ProductDto>>.Fail("Products not found", System.Net.HttpStatusCode.NotFound);
        }

        var productDtos = await items.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.UnitStock)).ToListAsync();

        return ServiceResult<List<ProductDto>>.Success(productDtos);

    }


    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var item = await productRepository.GetByIdAsync(id);

        if (item is null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }

        var productDto = new ProductDto(item.Id, item.Name, item.Price, item.UnitStock);

        return ServiceResult<ProductDto?>.Success(productDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedWithAllListAsync(int page, int pageSize)
    {
        var items = productRepository.GetAllListAsync();

        var skipValue = (page - 1) * pageSize;

        // Sayfalama işlemi yapıyoruz
        var pagedItems = await items
                            .Skip(skipValue)
                            .Take(pageSize)
                            .Select(x => new ProductDto(x.Id, x.Name, x.Price, x.UnitStock))
                            .ToListAsync();

        // Eğer veri yoksa hata dönüyoruz
        if (!items.Any())
        {
            return ServiceResult<List<ProductDto>>.Fail("Products not found", System.Net.HttpStatusCode.NotFound);
        }

        

        return ServiceResult<List<ProductDto>>.Success(pagedItems);
    }

    public async Task<ServiceResult> Update(int id, UpdateProductRequest updateRequest)
    {
        var item = await productRepository.GetByIdAsync(id);
        
        if(item is null)
        {
            return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }

        item.Name = updateRequest.Name;
        item.Price = updateRequest.Price;
        item.UnitStock = updateRequest.UnitStock;

        return ServiceResult.Success();
    }
}
