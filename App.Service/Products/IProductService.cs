namespace App.Service.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();

    Task<ServiceResult<List<ProductDto>>> GetPagedWithAllListAsync(int page, int pageSize);

    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);

    
    //Crud Start
    Task<ServiceResult> Update(int id, UpdateProductRequest updateRequest);


    Task<ServiceResult> Delete(int id);


    Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);

    //Crud End







}
