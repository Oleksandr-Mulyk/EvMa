using EvMa.CatalogService.Protos;
using EvMa.Core;
using EvMa.ECommerceLibrary;
using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.Products;

namespace EvMa.CatalogService.Services.Converters
{
    public class GprcProductConverter(
        ICatalogFactory catalogFactory,
        IGrpcImageConverter grpcImageConverter,
        IGrpcPriceConverter grpcPriceConverter,
        IGrpcAttributeValueConverter grpcAttributeValueConverter,
        IRepository<IAttributeSet> attributeSetRepository
        ) : IGprcProductConverter
    {
        public IProduct ToProduct(GrpcProduct product) =>
            catalogFactory.CreateProduct(
                product.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(product.Id),
                product.Sku,
                product.Name,
                product.Description,
                (decimal)product.Weight,
                (
                    Length: (decimal)product.Dimensions.Length,
                    Width: (decimal)product.Dimensions.Width,
                    Height: (decimal)product.Dimensions.Height
                ),
                (decimal)product.RegularPrice,
                [.. product.Prices.Select(grpcPriceConverter.ToPrice)],
                (decimal)product.StockQuantity,
                attributeSetRepository.GetByIdAsync(Guid.Parse(product.AttributeSetId)).Result,
                [.. product.AttributeValues.Select(grpcAttributeValueConverter.ToAttributeValue)],
                [.. product.Images.Select(grpcImageConverter.ToImage)],
                [.. product.Tags],
                product.IsActive
            );
    }
}
