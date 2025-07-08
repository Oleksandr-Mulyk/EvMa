using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGprcProductConverter
    {
        public IProduct ToProduct(GrpcProduct product);
    }
}
