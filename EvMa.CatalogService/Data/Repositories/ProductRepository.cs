using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class ProductRepository(DbSet<Product> dbSet, ApplicationContext dbContext) :
        DbContextRepository<IProduct, Product>(dbSet, dbContext), IProductRepository
    {
        public virtual async Task<IProduct> GetBySkuAsync(string sku) =>
            await dbSet.FirstOrDefaultAsync(p => p.Sku == sku) ?? throw new Exception(NotFoundMessage);

        public override IQueryable<IProduct> GetAll() =>
            base.GetAll()
            .Include(p => p.Prices)
            .Include(p => p.AttributeSet)
            .Include(p => p.AttributeValues)
            .Include(p => p.Images);

        public override async Task<IProduct> AddAsync(IProduct entity) =>
            IsValidProduct(entity) ?
            await base.AddAsync(entity) :
            throw new Exception(InvalidProductAttributesMessage);

        public override async Task<IProduct> UpdateAsync(IProduct entity) =>
            IsValidProduct(entity) ?
            await base.UpdateAsync(entity) :
            throw new Exception(InvalidProductAttributesMessage);

        protected virtual bool IsValidProduct(IProduct product)
        {
            var requiredAttributeIds = product.AttributeSet.Attributes?.Select(a => a.Id) ?? [];
            var valueAttributeIds = product.AttributeValues.Select(av => av.Attribute.Id) ?? [];

            return requiredAttributeIds.All(valueAttributeIds.Contains);
        }

        protected virtual string InvalidProductAttributesMessage => "Invalid product attributes.";
    }
}
