using EvMa.CatalogService.Protos;
using EvMa.Core;
using EvMa.ECommerceLibrary;
using EvMa.ECommerceLibrary.AttributeValues;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcAttributeValueConverter(
        ICatalogFactory catalogFactory,
        IRepository<IProductAttribute> productAttributeRepository
        ) : IGrpcAttributeValueConverter
    {
        public IAttributeValue ToAttributeValue(GrpcAttributeValue attributeValue) =>
            catalogFactory.CreateAttributeValue(
                attributeValue.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(attributeValue.Id),
                attributeValue.Value,
                productAttributeRepository.GetByIdAsync(Guid.Parse(attributeValue.AttributeId)).Result
            );
    }
}
