using App.Service;
using App.Service.Products;
using App.Service.Products.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : CustomActionController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllListAsync();

            return CustomActionResult(products);
        }


        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(CreateProductRequest createRequest)
             => CustomActionResult(await _productService.Create(createRequest));


        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
            => CustomActionResult(await _productService.Delete(id));



        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetById(int id)
            => CustomActionResult(await _productService.GetByIdAsync(id));
    }
}
