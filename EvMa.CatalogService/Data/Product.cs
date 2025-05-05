
namespace EvMa.CatalogService.Data
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public (decimal Length, decimal Width, decimal Height) Dimensions { get; set; }

        public decimal RegularPrice { get; set; }

        private IList<Price>? _prices;

        public IList<IPrice>? Prices
        {
            get => (IList<IPrice>?)_prices;
            set => _prices = value?.Cast<Price>().ToList();
        }

        public decimal StockQuantity { get; set; }

        private AttributeSet _attributeSet;

        public IAttributeSet AttributeSet
        {
            get => _attributeSet;
            set => _attributeSet = (AttributeSet)value;
        }

        private IList<AttributeValue>? _attributeValues;

        public IList<IAttributeValue> AttributeValues
        {
            get => (IList<IAttributeValue>?)_attributeValues;
            set => _attributeValues = value?.Cast<AttributeValue>().ToList();
        }

        private Category _category;

        public ICategory Category
        {
            get => _category;
            set => _category = (Category)value;
        }

        private IList<Image>? _images;

        public IList<IImage>? Images
        {
            get => (IList<IImage>?)_images;
            set => _images = value?.Cast<Image>().ToList();
        }

        public IList<string>? Tags { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
