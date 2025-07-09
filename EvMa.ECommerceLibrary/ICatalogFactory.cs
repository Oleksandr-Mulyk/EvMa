using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.AttributeValues;
using EvMa.ECommerceLibrary.Categories;
using EvMa.ECommerceLibrary.Images;
using EvMa.ECommerceLibrary.Prices;
using EvMa.ECommerceLibrary.ProductAttributes;
using EvMa.ECommerceLibrary.Products;

namespace EvMa.ECommerceLibrary
{
    public interface ICatalogFactory
    {
        public IProductAttribute CreateProductAttribute(Guid id, string name, string type);

        public IAttributeSet CreateAttributeSet(Guid id, string name, IList<IProductAttribute> attributes);

        public IAttributeValue CreateAttributeValue(Guid id, string value, IProductAttribute attribute);

        public IPrice CreatePrice(
            Guid id,
            decimal value,
            decimal? minQuantity,
            decimal? maxQuantity,
            DateTime startAt,
            DateTime endAt
            );

        public IImage CreateImage(Guid id, string url, string altText, int order);

        public IProduct CreateProduct(
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
            );

        public ICategory CreateCategory(
            Guid id,
            string name,
            string description,
            Guid? parentCategoryId,
            bool isActive,
            DateTime createdAt,
            DateTime updatedAt,
            IList<IProduct> products,
            IList<IImage>? images
            );
    }
}
