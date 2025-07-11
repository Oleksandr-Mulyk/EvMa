using EvMa.Core;
using EvMa.ECommerceLibrary.AttributeValues;
using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GrpcAttributeValueConverter(
        ICatalogFactory catalogFactory,
        IRepository<IProductAttribute> productAttributeRepository
        ) : IGrpcConverter<GrpcAttributeValue, IAttributeValue>
    {
        public IAttributeValue ConvertToEntity(GrpcAttributeValue source) =>
            catalogFactory.CreateAttributeValue(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                source.Value,
                productAttributeRepository.GetByIdAsync(Guid.Parse(source.AttributeId)).Result
            );

        public GrpcAttributeValue ConvertToGrpc(IAttributeValue entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                AttributeId = entity.Attribute.Id.ToString(),
                Value = entity.Value
            };
    }
}
