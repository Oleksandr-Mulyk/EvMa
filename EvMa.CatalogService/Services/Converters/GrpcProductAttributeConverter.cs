using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcProductAttributeConverter(ICatalogFactory catalogFactory) : IGrpcProductAttributeConverter
    {
        public IProductAttribute ToProductAttribute(GrpcProductAttribute productAttribute) =>
            catalogFactory.CreateProductAttribute(
                Guid.Parse(productAttribute.Id),
                productAttribute.Name,
                productAttribute.Type
            );
    }
}
