using EvMa.ECommerceLibrary.ProductAttributes;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class ProductAttributeRepository(ApplicationContext applicationContext) :
        DbContextRepository<IProductAttribute, ProductAttribute>(applicationContext), IQuerableProductAttributeRepository
    {
        protected override DbSet<ProductAttribute> DbSet => applicationContext.Attributes;
    }
}
