using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcPriceConverter(ICatalogFactory catalogFactory) : IGrpcPriceConverter
    {
        public IPrice ToPrice(GrpcPrice price) =>
            catalogFactory.CreatePrice(
                price.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(price.Id),
                (decimal)price.Value,
                (decimal?)price.MinQuantity,
                (decimal?)price.MaxQuantity,
                price.StartAt?.ToDateTime() ?? DateTime.MinValue,
                price.EndAt?.ToDateTime() ?? DateTime.MaxValue
                );
    }
}
