using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GrpcAttributeSetConverter(
        ICatalogFactory catalogFactory,
        IGrpcConverter<GrpcProductAttribute, IProductAttribute> grpcProductAttributeConverter
        ) : IGrpcConverter<GrpcAttributeSet, IAttributeSet>
    {
        public IAttributeSet ConvertToEntity(GrpcAttributeSet source) =>
            catalogFactory.CreateAttributeSet(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                source.Name,
                [.. source.Attributes.Select(grpcProductAttributeConverter.ConvertToEntity)]
                );

        public GrpcAttributeSet ConvertToGrpc(IAttributeSet entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Attributes = { entity.Attributes.Select(grpcProductAttributeConverter.ConvertToGrpc) }
            };
    }
}
