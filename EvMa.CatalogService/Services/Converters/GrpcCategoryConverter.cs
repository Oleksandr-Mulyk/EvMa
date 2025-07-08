using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Models;
using EvMa.ECommerceLibrary.Repositories;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcCategoryConverter(
        ICatalogFactory catalogFactory,
        IGrpcImageConverter grpcImageConverter,
        IProductRepository productRepository
        ) : IGrpcCategoryConverter
    {
        public ICategory ToCategory(GrpcCategory grpcCategory) =>
            catalogFactory.CreateCategory(
                grpcCategory.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(grpcCategory.Id),
                grpcCategory.Name,
                grpcCategory.Description,
                grpcCategory.ParentId == string.Empty ? null : Guid.Parse(grpcCategory.ParentId),
                grpcCategory.IsActive,
                grpcCategory.CreatedAt?.ToDateTime() ?? DateTime.Now,
                grpcCategory.UpdatedAt?.ToDateTime() ?? DateTime.Now,
                [.. productRepository.GetAllByIds([.. grpcCategory.ProductIds.Select(Guid.Parse)])],
                [.. grpcCategory.Images.Select(grpcImageConverter.ToImage)]
            );
    }
}
