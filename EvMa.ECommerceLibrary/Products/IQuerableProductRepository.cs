using EvMa.Core;

namespace EvMa.ECommerceLibrary.Products
{
    public interface IQuerableProductRepository : IRepository<IProduct>, IQuerableRepository<IProduct>
    {
        public Task<IProduct> GetBySkuAsync(string sku);

        public IQueryable<IProduct> GetAllByIds(List<Guid> ids);
    }
}
