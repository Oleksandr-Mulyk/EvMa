using EvMa.CatalogService.Data.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvMa.CatalogService.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<AppProduct>
    {
        public void Configure(EntityTypeBuilder<AppProduct> builder)
        {
            (string Length, string Width, string Height) = AppProduct.GetDimensionsFieldNames();

            builder.Property(Length).HasColumnType("decimal(18,2)");
            builder.Property(Width).HasColumnType("decimal(18,2)");
            builder.Property(Height).HasColumnType("decimal(18,2)");
        }
    }
}
