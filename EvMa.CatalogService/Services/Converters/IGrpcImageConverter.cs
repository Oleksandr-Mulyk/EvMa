using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcImageConverter
    {
        public IImage ToImage(GrpcImage image);
    }
}
