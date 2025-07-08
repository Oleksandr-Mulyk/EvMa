using EvMa.Core;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Data.Repositories
{
    public interface IProductRepository : IRepository<IProduct>
    {
        public Task<IProduct> GetBySkuAsync(string sku);

        public IQueryable<IProduct> GetAllByIds(List<Guid> ids);
    }
}
