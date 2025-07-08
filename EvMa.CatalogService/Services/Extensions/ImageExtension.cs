using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

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
