using App.Repositories.RepositoryPattern;

namespace App.Repositories.Categories;

public interface ICategoryRepository : IGenericRepositories<Category, int>
{
    Task<Category?> GetCategoriesWithProductsAsync(int id);

    IQueryable<Category?> GetCategoriesWithProducts();

}