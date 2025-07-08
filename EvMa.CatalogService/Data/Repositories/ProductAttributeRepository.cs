using EvMa.ECommerceLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class ProductAttributeRepository(ApplicationContext applicationContext) :
        DbContextRepository<IProductAttribute, ProductAttribute>(applicationContext)
    {
        protected override DbSet<ProductAttribute> DbSet => applicationContext.Attributes;
    }
}
