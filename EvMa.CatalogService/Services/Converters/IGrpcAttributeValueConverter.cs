using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcAttributeValueConverter
    {
        public IAttributeValue ToAttributeValue(GrpcAttributeValue attributeValue);
    }
}
