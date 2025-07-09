using EvMa.CatalogService.Protos;
using EvMa.ECommerceLibrary.Categories;
using Google.Protobuf.WellKnownTypes;
using Microsoft.IdentityModel.Tokens;

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
                CreatedAt = Timestamp.FromDateTime(category.CreatedAt.ToUniversalTime()),
                UpdatedAt = Timestamp.FromDateTime(category.UpdatedAt.ToUniversalTime()),
                Products = { category.Products?.Select(p => p.ToGrpcProduct()) },
                ProductIds = {
                    category.Products.IsNullOrEmpty() ?
                    category.ProductIds.Select(id => id.ToString()) :
                    category.Products.Select(p => p.Id.ToString())
                },
                Images = { category.Images?.Select(i => i.ToGrpcImage()) }
            };
    }
}
