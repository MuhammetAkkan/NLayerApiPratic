using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Models.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id); //primary key i yapılandırdık
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.UnitStock).IsRequired().HasDefaultValue(0); 
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
    }
}
