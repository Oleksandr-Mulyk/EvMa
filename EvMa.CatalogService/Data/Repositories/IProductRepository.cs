namespace EvMa.CatalogService.Data.Repositories
{
    public interface IProductRepository : IRepository<IProduct>
    {
        public Task<IProduct> GetBySkuAsync(string sku);
    }
}
