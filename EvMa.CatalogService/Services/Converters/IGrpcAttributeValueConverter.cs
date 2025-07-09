using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.AttributeValues;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcAttributeValueConverter
    {
        public IAttributeValue ToAttributeValue(GrpcAttributeValue attributeValue);
    }
}
