using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcPriceConverter
    {
        public IPrice ToPrice(GrpcPrice price);
    }
}
