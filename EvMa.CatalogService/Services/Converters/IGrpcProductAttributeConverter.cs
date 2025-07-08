using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcProductAttributeConverter
    {
        public IProductAttribute ToProductAttribute(GrpcProductAttribute productAttribute);
    }
}
