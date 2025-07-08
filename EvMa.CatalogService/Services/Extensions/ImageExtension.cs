using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class ImageExtension
    {
        public static GrpcImage ToGrpcImage(this IImage image) =>
            new()
            {
                Id = image.Id.ToString(),
                Url = image.Url,
                AltText = image.AltText
            };
    }
}
