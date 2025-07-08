using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvMa.CatalogService.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Products).WithMany();

            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
