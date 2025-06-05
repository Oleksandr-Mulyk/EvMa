using EvMa.CatalogService.Data.ApplicationModels;
using EvMa.CatalogService.Data.Configurations;
using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<AppCategory> Categories { get; set; }

        public DbSet<ProductAttribute> Attributes { get; set; }

        public DbSet<AppAttributeSet> AttributeSets { get; set; }

        public DbSet<AppAttributeValue> AttributeValues { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<AppProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttributeSetConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
