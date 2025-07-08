using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcAttributeSetConverter
    {
        public IAttributeSet ToAttributeSet(GrpcAttributeSet attributeSet);
    }
}
