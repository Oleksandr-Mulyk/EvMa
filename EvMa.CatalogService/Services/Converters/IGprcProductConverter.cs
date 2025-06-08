using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGprcProductConverter
    {
        public IProduct ToProduct(GrpcProduct product);
    }
}
