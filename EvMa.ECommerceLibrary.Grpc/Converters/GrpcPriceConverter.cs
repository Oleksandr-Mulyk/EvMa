using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.Prices;
using Google.Protobuf.WellKnownTypes;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GrpcPriceConverter(ICatalogFactory catalogFactory) : IGrpcConverter<GrpcPrice, IPrice>
    {
        public IPrice ConvertToEntity(GrpcPrice source) =>
            catalogFactory.CreatePrice(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                (decimal)source.Value,
                (decimal?)source.MinQuantity,
                (decimal?)source.MaxQuantity,
                source.StartAt?.ToDateTime() ?? DateTime.MinValue,
                source.EndAt?.ToDateTime() ?? DateTime.MaxValue
                );

        public GrpcPrice ConvertToGrpc(IPrice entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                Value = (double)entity.Value,
                MinQuantity = (double)entity.MinQuantity,
                MaxQuantity = (double)entity.MaxQuantity,
                StartAt = entity.StartAt.HasValue ?
                    Timestamp.FromDateTime(entity.StartAt.Value.ToUniversalTime()) :
                    null,
                EndAt = entity.EndAt.HasValue ? Timestamp.FromDateTime(entity.EndAt.Value.ToUniversalTime()) : null,
            };
    }
}
