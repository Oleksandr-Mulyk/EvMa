using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.AttributeValues;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class AttributeValueExtension
    {
        public static GrpcAttributeValue ToGrpcAttributeValue(this IAttributeValue attributeValue) =>
            new()
            {
                Id = attributeValue.Id.ToString(),
                AttributeId = attributeValue.Attribute.Id.ToString(),
                Value = attributeValue.Value
            };
    }
}
