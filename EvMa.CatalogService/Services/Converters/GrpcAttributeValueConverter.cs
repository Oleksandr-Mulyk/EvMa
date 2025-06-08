using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcAttributeValueConverter(
        ICatalogFactory catalogFactory,
        IGrpcProductAttributeConverter grpcProductAttributeConverter
        ) : IGrpcAttributeValueConverter
    {
        public IAttributeValue ToAttributeValue(GrpcAttributeValue attributeValue) =>
            catalogFactory.CreateAttributeValue(
                Guid.Parse(attributeValue.Id),
                attributeValue.Value,
                grpcProductAttributeConverter.ToProductAttribute(attributeValue.Attribute)
            );
    }
}
