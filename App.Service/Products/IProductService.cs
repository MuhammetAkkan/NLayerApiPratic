using App.Service.Products.Create;
using App.Service.Products.Update;

namespace App.Service.Products;
public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();

    Task<ServiceResult<ProductDto>> GetByIdAsync(int id);

    //create
    Task<ServiceResult<CreateProductResponse>> Create(CreateProductRequest createRequest);

    //update
    Task<ServiceResult> Update(int Id ,UpdateProductRequest updateRequest);

    Task<ServiceResult> Delete(int id);

    Task<ServiceResult<List<ProductDto>>> GetPagedWithAl(int pageNumber, int pageSize);

    Task<ServiceResult<ProductDto>> GetByMaxPrice();

    Task<ServiceResult<ProductDto>> GetByMinPrice();

    Task<ServiceResult<List<ProductDto>>> GetPriceWithKdv();


}
