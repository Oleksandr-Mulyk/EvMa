using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Categories;

namespace EvMa.CatalogService.Services.Converters
{
    public interface IGrpcCategoryConverter
    {
        public ICategory ToCategory(GrpcCategory grpcCategory);
    }
}
