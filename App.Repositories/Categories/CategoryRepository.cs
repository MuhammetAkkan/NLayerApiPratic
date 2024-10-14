using App.Repositories.RepositoryPattern;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepositories<Category, int>(context), ICategoryRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Category?> GetCategoriesWithProductsAsync(int id)
    {
        var result = await _context.Categories
                .Include(i => i.Products)
            .FirstOrDefaultAsync(i => i.Id == id);
        return result;
    }

    public IQueryable<Category?> GetCategoriesWithProducts()
    {
        var productQueryable = _context.Categories
                .Include(i => i.Products)
            .AsNoTracking();

        return productQueryable;
    }
}


    
    


