using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        public DbSet<AttributeSet> AttributeSets { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
