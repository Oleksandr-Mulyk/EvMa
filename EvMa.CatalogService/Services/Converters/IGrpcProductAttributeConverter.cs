using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcProductAttributeConverter
    {
        public IProductAttribute ToProductAttribute(GrpcProductAttribute productAttribute);
    }
}
