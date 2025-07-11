using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.AttributeValues;
using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.Images;
using EvMa.ECommerceLibrary.Prices;
using EvMa.ECommerceLibrary.Products;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GprcProductConverter(
        ICatalogFactory catalogFactory,
        IGrpcConverter<GrpcImage, IImage> grpcImageConverter,
        IGrpcConverter<GrpcPrice, IPrice> grpcPriceConverter,
        IGrpcConverter<GrpcAttributeValue, IAttributeValue> grpcAttributeValueConverter,
        IQuerableAttributeSetRepository attributeSetRepository
        ) : IGrpcConverter<GrpcProduct, IProduct>
    {
        public IProduct ConvertToEntity(GrpcProduct source) =>
            catalogFactory.CreateProduct(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                source.Sku,
                source.Name,
                source.Description,
                (decimal)source.Weight,
                (
                    Length: (decimal)source.Dimensions.Length,
                    Width: (decimal)source.Dimensions.Width,
                    Height: (decimal)source.Dimensions.Height
                ),
                (decimal)source.RegularPrice,
                [.. source.Prices.Select(grpcPriceConverter.ConvertToEntity)],
                (decimal)source.StockQuantity,
                attributeSetRepository.GetByIdAsync(Guid.Parse(source.AttributeSetId)).Result,
                [.. source.AttributeValues.Select(grpcAttributeValueConverter.ConvertToEntity)],
                [.. source.Images.Select(grpcImageConverter.ConvertToEntity)],
                [.. source.Tags],
                source.IsActive
            );

        public GrpcProduct ConvertToGrpc(IProduct entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                Sku = entity.Sku,
                Name = entity.Name,
                Description = entity.Description,
                Weight = (double)entity.Weight,
                Dimensions = new GrpcDimensions
                {
                    Length = (double)entity.Dimensions.Length,
                    Width = (double)entity.Dimensions.Width,
                    Height = (double)entity.Dimensions.Height
                },
                RegularPrice = (double)entity.RegularPrice,
                Prices = { entity.Prices?.Select(grpcPriceConverter.ConvertToGrpc) },
                StockQuantity = (double)entity.StockQuantity,
                AttributeSetId = entity.AttributeSet.Id.ToString(),
                AttributeValues = { entity.AttributeValues?.Select(grpcAttributeValueConverter.ConvertToGrpc) },
                Images = { entity.Images?.Select(grpcImageConverter.ConvertToGrpc) },
                Tags = { entity.Tags ?? [] },
                IsActive = entity.IsActive
            };
    }
}
