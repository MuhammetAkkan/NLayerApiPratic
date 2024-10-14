using App.Service;
using App.Service.Categories;
using App.Service.Categories.Create;
using App.Service.Categories.Update;
using App.Service.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService _categoryService) : CustomActionController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllCategories() => CustomActionResult(await _categoryService.GetAllCategoriesAsync());


        [HttpGet("withProducts")]
        public async Task<IActionResult> GetCategoriesWithProducts() => CustomActionResult(await _categoryService.GetCategoriesWithProducts());


        [HttpGet("withProducts/{id}")]
        public async Task<IActionResult> GetCategoriesWithProducts(int id) => CustomActionResult(await _categoryService.GetCategoriesWithProducts(id));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CustomActionResult(await _categoryService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest createCategoryRequest) => CustomActionResult(await _categoryService.CreateCategoryAsync(createCategoryRequest));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest updateRequest) => CustomActionResult(await _categoryService.UpdateCategoryAsync(id, updateRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) => CustomActionResult(await _categoryService.DeleteCategoryAsync(id));









    }
}
