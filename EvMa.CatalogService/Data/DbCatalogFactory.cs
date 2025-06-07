using EvMa.CatalogService.Data.Models;

namespace EvMa.CatalogService.Data
{
    public class DbCatalogFactory : ICatalogFactory
    {
        public IProductAttribute CreateProductAttribute() => new ProductAttribute();

        public IAttributeSet CreateAttributeSet() => new AttributeSet();

        public IAttributeValue CreateAttributeValue() => new AttributeValue();

        public IPrice CreatePrice() => new Price();

        public IImage CreateImage() => new Image();

        public IProduct CreateProduct() => new Product();

        public ICategory CreateCategory() => new Category();
    }
}
