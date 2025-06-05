
using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; } = 0m;

        [Column("Length", TypeName = "decimal(18,2)")]
        private decimal _length = 0m;

        [Column("Width", TypeName = "decimal(18,2)")]
        private decimal _width = 0m;

        [Column("Height", TypeName = "decimal(18,2)")]
        private decimal _height = 0m;

        [NotMapped]
        public (decimal Length, decimal Width, decimal Height) Dimensions
        {
            get => (_length, _width, _height);
            set
            {
                _length = value.Length;
                _width = value.Width;
                _height = value.Height;
            }
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RegularPrice { get; set; } = 0m;

        [Column("Prices")]
        private IList<Price>? _prices;

        [NotMapped]
        public IList<IPrice>? Prices
        {
            get => (IList<IPrice>?)_prices;
            set => _prices = value?.Cast<Price>().ToList();
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StockQuantity { get; set; }

        [Column("AttributeSet")]
        private AttributeSet _attributeSet;

        [NotMapped]
        public IAttributeSet AttributeSet
        {
            get => _attributeSet;
            set => _attributeSet = (AttributeSet)value;
        }

        [Column("AttributeValues")]
        private IList<AttributeValue>? _attributeValues;

        [NotMapped]
        public IList<IAttributeValue> AttributeValues
        {
            get => (IList<IAttributeValue>?)_attributeValues;
            set => _attributeValues = value?.Cast<AttributeValue>().ToList();
        }

        [Column("Category")]
        private Category _category;

        [NotMapped]
        public ICategory Category
        {
            get => _category;
            set => _category = (Category)value;
        }

        [Column("Images")]
        private IList<Image>? _images;

        [NotMapped]
        public IList<IImage>? Images
        {
            get => (IList<IImage>?)_images;
            set => _images = value?.Cast<Image>().ToList();
        }

        public IList<string>? Tags { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
