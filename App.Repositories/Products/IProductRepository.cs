﻿using App.Repositories.RepositoryPattern;

namespace App.Repositories.Models.Products;

public interface IProductRepository : IGenericRepositories<Product, int>
{
    Task<List<Product>> GetBetweenPriceProducts(decimal minPrice, decimal maxPrice);

    Task<Product> GetByName(string name);


}
