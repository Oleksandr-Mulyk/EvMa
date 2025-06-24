using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcAttributeSetConverter(
        ICatalogFactory catalogFactory,
        IGrpcProductAttributeConverter grpcProductAttributeConverter
        ) : IGrpcAttributeSetConverter
    {
        public IAttributeSet ToAttributeSet(GrpcAttributeSet attributeSet) =>
            catalogFactory.CreateAttributeSet(
                attributeSet.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(attributeSet.Id),
                attributeSet.Name,
                [ .. attributeSet.Attributes.Select(grpcProductAttributeConverter.ToProductAttribute)]
                );
    }
}
