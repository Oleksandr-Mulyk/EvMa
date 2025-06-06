using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvMa.CatalogService.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            (string Length, string Width, string Height) = Product.GetDimensionsFieldNames();

            builder.Property(Length).HasColumnType("decimal(18,2)");
            builder.Property(Width).HasColumnType("decimal(18,2)");
            builder.Property(Height).HasColumnType("decimal(18,2)");
        }
    }
}
