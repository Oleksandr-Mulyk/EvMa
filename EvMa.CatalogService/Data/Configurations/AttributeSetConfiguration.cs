using EvMa.CatalogService.Data.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvMa.CatalogService.Data.Configurations
{
    public class AttributeSetConfiguration : IEntityTypeConfiguration<AppAttributeSet>
    {
        public void Configure(EntityTypeBuilder<AppAttributeSet> builder) =>
            builder.HasMany(a => a.Attributes).WithMany();
    }
}
