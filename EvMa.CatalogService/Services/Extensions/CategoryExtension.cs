using EvMa.CatalogService.Data;
using EvMa.CatalogService.Protos;
using Google.Protobuf.WellKnownTypes;

namespace EvMa.CatalogService.Services.Extensions
{
    public static class CategoryExtension
    {
        public static GrpcCategory ToGrpcCategory(this ICategory category) =>
            new()
            {
                Id = category.Id.ToString(),
                Name = category.Name,
                Description = category.Description,
                ParentId = category.ParentCategoryId.ToString(),
                IsActive = category.IsActive,
                CreatedAt = Timestamp.FromDateTime(category.CreatedAt),
                UpdatedAt = Timestamp.FromDateTime(category.UpdatedAt),
                Products = { category.Products?.Select(p => p.ToGrpcProduct()) },
                Images = { category.Images?.Select(i => i.ToGrpcImage()) },
            };
    }
}
