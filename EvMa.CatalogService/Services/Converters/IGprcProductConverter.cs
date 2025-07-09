using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Products;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGprcProductConverter
    {
        public IProduct ToProduct(GrpcProduct product);
    }
}
