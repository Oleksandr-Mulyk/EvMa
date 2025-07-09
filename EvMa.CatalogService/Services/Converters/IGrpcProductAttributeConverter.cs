using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcProductAttributeConverter
    {
        public IProductAttribute ToProductAttribute(GrpcProductAttribute productAttribute);
    }
}
