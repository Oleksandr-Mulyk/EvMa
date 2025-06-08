using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcImageConverter(ICatalogFactory catalogFactory) : IGrpcImageConverter
    {
        public IImage ToImage(GrpcImage image) =>
            catalogFactory.CreateImage(Guid.Parse(image.Id), image.Url, image.AltText, image.Order);
    }
}
