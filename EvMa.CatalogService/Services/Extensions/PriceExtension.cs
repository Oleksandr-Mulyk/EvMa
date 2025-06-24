using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;
using Google.Protobuf.WellKnownTypes;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class PriceExtension
    {
        public static GrpcPrice ToGrpcPrice(this IPrice price) =>
            new()
            {
                Id = price.Id.ToString(),
                Value = (double)price.Value,
                MinQuantity = (double)price.MinQuantity,
                MaxQuantity = (double)price.MaxQuantity,
                StartAt = price.StartAt.HasValue ?
                    Timestamp.FromDateTime(price.StartAt.Value.ToUniversalTime()) :
                    null,
                EndAt = price.EndAt.HasValue ? Timestamp.FromDateTime(price.EndAt.Value.ToUniversalTime()) : null,
            };
    }
}
