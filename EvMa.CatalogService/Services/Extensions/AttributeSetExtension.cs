using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class AttributeSetExtension
    {
        public static GrpcAttributeSet ToGrpcAttributeSet(this IAttributeSet attributeSet) =>
            new()
            {
                Id = attributeSet.Id.ToString(),
                Name = attributeSet.Name,
                Attributes = { attributeSet.Attributes.Select(a => a.ToGrpcProductAttribute()) }
            };
    }
}
