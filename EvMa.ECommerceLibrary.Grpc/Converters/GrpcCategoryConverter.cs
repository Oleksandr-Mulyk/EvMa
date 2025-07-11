using EvMa.ECommerceLibrary.Categories;
using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.Images;
using EvMa.ECommerceLibrary.Products;
using Google.Protobuf.WellKnownTypes;

namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public class GrpcCategoryConverter(
        ICatalogFactory catalogFactory,
        IGrpcConverter<GrpcImage, IImage> grpcImageConverter,
        IGrpcConverter<GrpcProduct, IProduct> grpcProductConverter,
        IQuerableProductRepository productRepository
        ) : IGrpcConverter<GrpcCategory, ICategory>
    {
        public ICategory ConvertToEntity(GrpcCategory source) =>
            catalogFactory.CreateCategory(
                source.Id == string.Empty ? Guid.NewGuid() : Guid.Parse(source.Id),
                source.Name,
                source.Description,
                source.ParentId == string.Empty ? null : Guid.Parse(source.ParentId),
                source.IsActive,
                source.CreatedAt?.ToDateTime() ?? DateTime.Now,
                source.UpdatedAt?.ToDateTime() ?? DateTime.Now,
                [.. productRepository.GetAllByIds([.. source.ProductIds.Select(Guid.Parse)])],
                [.. source.Images.Select(grpcImageConverter.ConvertToEntity)]
            );

        public GrpcCategory ConvertToGrpc(ICategory entity) =>
            new()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                ParentId = entity.ParentCategoryId.ToString(),
                IsActive = entity.IsActive,
                CreatedAt = Timestamp.FromDateTime(entity.CreatedAt.ToUniversalTime()),
                UpdatedAt = Timestamp.FromDateTime(entity.UpdatedAt.ToUniversalTime()),
                Products = { entity.Products?.Select(grpcProductConverter.ConvertToGrpc) },
                ProductIds = {
                    entity.Products is not null && entity.Products?.Count > 0 ?
                    entity.ProductIds.Select(id => id.ToString()) :
                    entity.Products.Select(p => p.Id.ToString())
                },
                Images = { entity.Images?.Select(grpcImageConverter.ConvertToGrpc) }
            };
    }
}
