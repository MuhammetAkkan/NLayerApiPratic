using App.Repositories.Models.Products;
using App.Service.Products;

namespace App.Service.Categories.CategoryForDtos;

public record CategoryWithProductDto(int Id, string Name, List<ProductDto>? Products); 