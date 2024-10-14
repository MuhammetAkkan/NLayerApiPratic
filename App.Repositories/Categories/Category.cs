using App.Repositories.Models.Products;

namespace App.Repositories.Categories;

public class Category : BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = default!;
    public List<Product>? Products { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}