using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary;
using EvMa.ECommerceLibrary.Images;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcImageConverter(ICatalogFactory catalogFactory) : IGrpcImageConverter
    {
        public IImage ToImage(GrpcImage image) =>
            catalogFactory.CreateImage(
                image.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(image.Id),
                image.Url, image.AltText,
                image.Order
                );
    }
}
