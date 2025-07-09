using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Prices;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcPriceConverter
    {
        public IPrice ToPrice(GrpcPrice price);
    }
}
