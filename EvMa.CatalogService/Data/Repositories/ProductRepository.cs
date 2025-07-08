using EvMa.CatalogService.Data.Models;
using EvMa.ECommerceLibrary.Models;
using EvMa.ECommerceLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class ProductRepository(ApplicationContext dbContext) :
        DbContextRepository<IProduct, Product>(dbContext), IProductRepository
    {
        protected override DbSet<Product> DbSet => dbContext.Products;

        public override async Task<IProduct> GetByIdAsync(Guid id) =>
            await DbSet
            .Include(p => p.Prices)
            .Include(p => p.AttributeSet).ThenInclude(aset => aset.Attributes)
            .Include(p => p.AttributeValues).ThenInclude(aset => aset.Attribute)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception(NotFoundMessage);

        public virtual async Task<IProduct> GetBySkuAsync(string sku) =>
            await DbSet
            .Include(p => p.Prices)
            .Include(p => p.AttributeSet).ThenInclude(aset => aset.Attributes)
            .Include(p => p.AttributeValues).ThenInclude(aset => aset.Attribute)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Sku == sku) ?? throw new Exception(NotFoundMessage);

        public override IQueryable<IProduct> GetAll() =>
            DbSet
            .Include(p => p.Prices)
            .Include(p => p.AttributeSet).ThenInclude(aset => aset.Attributes)
            .Include(p => p.AttributeValues).ThenInclude(aset => aset.Attribute)
            .Include(p => p.Images)
            .AsQueryable()
            .Select(p => p as IProduct);

        public virtual IQueryable<IProduct> GetAllByIds(List<Guid> ids) =>
            DbSet
            .Include(p => p.Prices)
            .Include(p => p.AttributeSet).ThenInclude(aset => aset.Attributes)
            .Include(p => p.AttributeValues).ThenInclude(aset => aset.Attribute)
            .Include(p => p.Images)
            .Where(p => ids.Contains(p.Id))
            .AsQueryable()
            .Select(p => p as IProduct);

        public override async Task<IProduct> AddAsync(IProduct entity)
        {
            dbContext.Entry(entity.AttributeSet).State = EntityState.Unchanged;

            return IsValidProduct(entity) ?
                await base.AddAsync(entity) :
                throw new Exception(InvalidProductAttributesMessage);
        }

        public override async Task<IProduct> UpdateAsync(IProduct entity)
        {
            dbContext.Entry(entity.AttributeSet).State = EntityState.Unchanged;

            return IsValidProduct(entity) ?
                await base.UpdateAsync(entity) :
                throw new Exception(InvalidProductAttributesMessage);
        }

        protected virtual bool IsValidProduct(IProduct product)
        {
            var requiredAttributeIds = product.AttributeSet.Attributes?.Select(a => a.Id) ?? [];
            var valueAttributeIds = product.AttributeValues?.Select(av => av.Attribute.Id) ?? [];

            return requiredAttributeIds.All(valueAttributeIds.Contains);
        }

        protected virtual string InvalidProductAttributesMessage => "Invalid product attributes.";
    }
}
