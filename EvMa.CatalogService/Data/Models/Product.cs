using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data.Models
{
    public class Product<TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage> :
        IProduct<TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage>
        where TProductAttribute : IProductAttribute
        where TAttributeSet : IAttributeSet<TProductAttribute>
        where TAttributeValue : IAttributeValue<TProductAttribute>
        where TPrice : IPrice
        where TImage : IImage
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; } = 0m;

        protected decimal _length = 0m;

        protected decimal _width = 0m;

        protected decimal _height = 0m;

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

        public IList<TPrice>? Prices { get; set; } = [];

        [Column(TypeName = "decimal(18,2)")]
        public decimal StockQuantity { get; set; }

        public TAttributeSet AttributeSet{ get; set; }

        public IList<TAttributeValue> AttributeValues { get; set; } = [];

        public IList<TImage>? Images { get; set; } = [];

        public IList<string>? Tags { get; set; }

        public bool IsActive { get; set; } = true;

        public static (string, string, string) GetDimensionsFieldNames()
        {
            return (nameof(_length), nameof(_width), nameof(_height));
        }
    }
}
