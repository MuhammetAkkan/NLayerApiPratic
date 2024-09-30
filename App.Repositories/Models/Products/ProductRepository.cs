using App.Repositories.RepositoryPattern;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Models.Products;

public class ProductRepository(AppDbContext context) : GenericRepositories<Product>(context), IProductRepository
{

    private readonly AppDbContext _context = context;
    public async Task<List<Product>> GetBetweenPriceProducts(decimal minPrice, decimal maxPrice)
    {
        var result = await _context.Products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToListAsync();

        return result;
    }

    public async Task<Product> GetByName(string name)
    {
        var result = await _context.Products.FirstOrDefaultAsync(x => x.Name == name);

        return result!;
    }
}
