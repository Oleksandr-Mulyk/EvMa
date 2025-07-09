using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class ProductAttributeExtension
    {
        public static GrpcProductAttribute ToGrpcProductAttribute(this IProductAttribute attribute) =>
            new()
            {
                Id = attribute.Id.ToString(),
                Name = attribute.Name,
                Type = attribute.Type.ToString()
            };
    }
}
