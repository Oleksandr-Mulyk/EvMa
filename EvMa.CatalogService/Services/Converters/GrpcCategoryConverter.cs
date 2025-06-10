using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;

namespace EvMa.CatalogService.Services.Converters
{
    public class GrpcCategoryConverter(
        ICatalogFactory catalogFactory,
        IGprcProductConverter grpcProductConverter,
        IGrpcImageConverter grpcImageConverter
        ) : IGrpcCategoryConverter
    {
        public ICategory ToCategory(GrpcCategory grpcCategory) =>
            catalogFactory.CreateCategory(
                Guid.Parse(grpcCategory.Id),
                grpcCategory.Name,
                grpcCategory.Description,
                Guid.Parse(grpcCategory.ParentId),
                grpcCategory.IsActive,
                grpcCategory.CreatedAt.ToDateTime(),
                grpcCategory.UpdatedAt.ToDateTime(),
                [.. grpcCategory.Products.Select(grpcProductConverter.ToProduct)],
                [.. grpcCategory.Images.Select(grpcImageConverter.ToImage)]
            );
    }
}
