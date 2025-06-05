using EvMa.CatalogService.Data.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvMa.CatalogService.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<AppCategory>
    {
        public void Configure(EntityTypeBuilder<AppCategory> builder)
        {
            builder.HasMany(c => c.Products).WithMany();

            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
