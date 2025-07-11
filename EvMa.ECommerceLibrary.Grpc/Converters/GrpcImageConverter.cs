using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.Images;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GrpcImageConverter(ICatalogFactory catalogFactory) : IGrpcConverter<GrpcImage, IImage>
    {
        public IImage ConvertToEntity(GrpcImage source) =>
            catalogFactory.CreateImage(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                source.Url, source.AltText,
                source.Order
                );

        public GrpcImage ConvertToGrpc(IImage entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                Url = entity.Url,
                AltText = entity.AltText
            };
    }
}
