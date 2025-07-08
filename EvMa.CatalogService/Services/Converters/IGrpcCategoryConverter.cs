using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcCategoryConverter
    {
        public ICategory ToCategory(GrpcCategory grpcCategory);
    }
}
