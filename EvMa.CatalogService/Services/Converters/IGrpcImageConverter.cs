using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Images;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcImageConverter
    {
        public IImage ToImage(GrpcImage image);
    }
}
