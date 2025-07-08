using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcCategoryConverter
    {
        public ICategory ToCategory(GrpcCategory grpcCategory);
    }
}
