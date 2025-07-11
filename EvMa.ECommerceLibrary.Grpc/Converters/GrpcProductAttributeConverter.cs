using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GrpcProductAttributeConverter(ICatalogFactory catalogFactory)
        : IGrpcConverter<GrpcProductAttribute, IProductAttribute>
    {
        public IProductAttribute ConvertToEntity(GrpcProductAttribute source) =>
            catalogFactory.CreateProductAttribute(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                source.Name,
                source.Type
            );

        public GrpcProductAttribute ConvertToGrpc(IProductAttribute entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Type = entity.Type.ToString()
            };
    }
}
