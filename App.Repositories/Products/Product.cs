using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Repositories.Categories;

namespace App.Repositories.Models.Products;

public class Product : BaseEntity<int>, IAuditEntity
{
    
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int UnitStock { get; set; }



    [Required]
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category Category { get; set; } = default!;



    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}
