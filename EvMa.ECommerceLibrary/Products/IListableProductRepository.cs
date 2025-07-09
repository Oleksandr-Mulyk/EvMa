using EvMa.Core;

namespace EvMa.ECommerceLibrary.Products
{
    public interface IListableProductRepository : IRepository<IProduct>, IListableRepository<IProduct>
    {
        public Task<IProduct> GetBySkuAsync(string sku);

        public Task<IList<IProduct>> GetListByIds(List<Guid> ids);
    }
}
