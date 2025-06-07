namespace EvMa.CatalogService.Data
{
    public interface ICatalogFactory
    {
        public IProductAttribute CreateProductAttribute();

        public IAttributeSet CreateAttributeSet();

        public IAttributeValue CreateAttributeValue();

        public IPrice CreatePrice();

        public IImage CreateImage();

        public IProduct CreateProduct();

        public ICategory CreateCategory();
    }
}
