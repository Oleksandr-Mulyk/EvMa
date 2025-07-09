using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Products;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class ProductExtension
    {
        public static GrpcProduct ToGrpcProduct(this IProduct product) =>
            new()
            {
                Id = product.Id.ToString(),
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Weight = (double)product.Weight,
                Dimensions = new GrpcDimensions
                {
                    Length = (double)product.Dimensions.Length,
                    Width = (double)product.Dimensions.Width,
                    Height = (double)product.Dimensions.Height
                },
                RegularPrice = (double)product.RegularPrice,
                Prices = { product.Prices?.Select(p => p.ToGrpcPrice()) },
                StockQuantity = (double)product.StockQuantity,
                AttributeSetId = product.AttributeSet.Id.ToString(),
                AttributeValues = { product.AttributeValues?.Select(av => av.ToGrpcAttributeValue()) },
                Images = { product.Images?.Select(i => i.ToGrpcImage()) },
                Tags = { product.Tags ?? [] },
                IsActive = product.IsActive
            };
    }
}
