using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.AttributeSets;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcAttributeSetConverter
    {
        public IAttributeSet ToAttributeSet(GrpcAttributeSet attributeSet);
    }
}
