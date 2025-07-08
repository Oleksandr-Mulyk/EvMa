using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcImageConverter
    {
        public IImage ToImage(GrpcImage image);
    }
}
