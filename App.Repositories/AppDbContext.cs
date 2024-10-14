using App.Repositories.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using App.Repositories.Categories;

namespace App.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configuretion classlarının tamamını bulup uyguluyor
        //entity özelleştirmeleri Configure metodunda yapılır
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
