using App.Service.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAll()
        {
            return CustomActionResult(await productService.GetAllListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            return CustomActionResult(await productService.CreateProductAsync(request));
        }
    }
}
