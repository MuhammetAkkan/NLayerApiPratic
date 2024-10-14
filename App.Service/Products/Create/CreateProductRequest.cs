namespace App.Service.Products.Create;

public record CreateProductRequest(string Name, decimal Price, int UnitStock, int CategoryId);

