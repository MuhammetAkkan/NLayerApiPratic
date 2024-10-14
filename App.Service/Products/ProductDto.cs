namespace App.Service.Products;

public record ProductDto(int Id, string Name, int UnitStock, decimal Price, int CategoryId);
