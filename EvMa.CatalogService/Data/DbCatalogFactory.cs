using EvMa.CatalogService.Data.Models;

namespace EvMa.CatalogService.Data
{
    public class DbCatalogFactory : ICatalogFactory
    {
        public virtual IProductAttribute CreateProductAttribute(Guid id, string name, string type) =>
            new ProductAttribute()
            {
                Id = id,
                Name = name,
                Type = type
            };

        public virtual IAttributeSet CreateAttributeSet(Guid id, string name, IList<IProductAttribute> attributes) =>
            new AttributeSet()
            {
                Id = id,
                Name = name,
                Attributes = attributes.Cast<ProductAttribute>().ToList()
            };

        public virtual IAttributeValue CreateAttributeValue(Guid id, string value, IProductAttribute attribute) =>
            new AttributeValue()
            {
                Id = id,
                Value = value,
                Attribute = attribute as ProductAttribute
            };

        public virtual IPrice CreatePrice(
            Guid id,
            decimal value,
            decimal? minQuantity,
            decimal? maxQuantity,
            DateTime startAt,
            DateTime endAt
            ) =>
            new Price()
            {
                Id = id,
                Value = value,
                MinQuantity = minQuantity,
                MaxQuantity = maxQuantity,
                StartAt = startAt,
                EndAt = endAt
            };

        public virtual IImage CreateImage(Guid id, string url, string altText, int order) =>
            new Image()
            {
                Id = id,
                Url = url,
                AltText = altText,
                Order = order
            };

        public virtual IProduct CreateProduct(
            Guid id,
            string sku,
            string name,
            string description,
            decimal weight,
            (decimal Length, decimal Width, decimal Height) dimensions,
            decimal regularPrice,
            IList<IPrice>? prices,
            decimal stockQuantity,
            IAttributeSet attributeSet,
            IList<IAttributeValue> attributeValues,
            IList<IImage>? images,
            IList<string>? tags,
            bool isActive
            ) =>
            new Product()
            {
                Id = id,
                Sku = sku,
                Name = name,
                Description = description,
                Weight = weight,
                Dimensions = dimensions,
                RegularPrice = regularPrice,
                Prices = [.. prices?.Cast<Price>()],
                StockQuantity = stockQuantity,
                AttributeSet = attributeSet as AttributeSet,
                AttributeValues = [.. attributeValues.Cast<AttributeValue>()],
                Images = [.. images?.Cast<Image>()],
                Tags = tags,
                IsActive = isActive
            };

        public virtual ICategory CreateCategory(
            Guid id,
            string name,
            string description,
            int parentCategoryId,
            bool isActive,
            DateTime createdAt,
            DateTime updatedAt,
            IList<IProduct> products,
            IList<IImage>? images
            ) =>
            new Category()
            {
                Id = id,
                Name = name,
                Description = description,
                ParentCategoryId = parentCategoryId,
                IsActive = isActive,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
                Products = products.Cast<Product>().ToList(),
                Images = images?.Cast<Image>().ToList()
            };
    }
}
