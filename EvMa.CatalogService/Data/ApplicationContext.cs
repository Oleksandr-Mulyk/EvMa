using EvMa.CatalogService.Data.Configurations;
using EvMa.CatalogService.Data.Models;
using EvMa.ECommerceLibrary.Models;
using Microsoft.EntityFrameworkCore;

using Price = EvMa.CatalogService.Data.Models.Price;
using Category = EvMa.CatalogService.Data.Models.Category;
using Product = EvMa.CatalogService.Data.Models.Product;

namespace EvMa.CatalogService.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductAttribute> Attributes { get; set; }

        public DbSet<AttributeSet> AttributeSets { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttributeSetConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
