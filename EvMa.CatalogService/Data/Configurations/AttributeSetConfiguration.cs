using EvMa.ECommerceLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvMa.CatalogService.Data.Configurations
{
    public class AttributeSetConfiguration : IEntityTypeConfiguration<AttributeSet>
    {
        public void Configure(EntityTypeBuilder<AttributeSet> builder) =>
            builder.HasMany(a => a.Attributes).WithMany();
    }
}
