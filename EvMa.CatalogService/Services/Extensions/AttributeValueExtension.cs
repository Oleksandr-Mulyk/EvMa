using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class AttributeValueExtension
    {
        public static GrpcAttributeValue ToGrpcAttributeValue(this IAttributeValue attributeValue) =>
            new()
            {
                Id = attributeValue.Id.ToString(),
                Attribute = attributeValue.Attribute.ToGrpcProductAttribute(),
                Value = attributeValue.Value
            };
    }
}
