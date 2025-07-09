using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.AttributeValues;
using EvMa.ECommerceLibrary.Images;
using EvMa.ECommerceLibrary.Prices;
using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.ECommerceLibrary.Products
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

        public decimal Weight { get; set; } = 0m;

        protected decimal _length = 0m;

        protected decimal _width = 0m;

        protected decimal _height = 0m;

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

        public decimal RegularPrice { get; set; } = 0m;

        public IList<TPrice>? Prices { get; set; } = [];

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

    public class Product : Product<ProductAttribute, AttributeSet, AttributeValue, Price, Image>, IProduct
    {
        IList<IPrice>? IProduct<IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage>.Prices
        {
            get => [.. Prices?.Cast<IPrice>() ?? []];
            set => Prices = [.. value?.Cast<Price>() ?? []];
        }

        IAttributeSet IProduct<IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage>.AttributeSet
        {
            get => AttributeSet;
            set => AttributeSet = (AttributeSet)value;
        }

        IList<IAttributeValue>
            IProduct<IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage>.AttributeValues
        {
            get => [.. AttributeValues?.Cast<IAttributeValue>() ?? []];
            set => AttributeValues = [.. value?.Cast<AttributeValue>() ?? []];
        }

        IList<IImage>? IProduct<IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage>.Images
        {
            get => [.. Images?.Cast<IImage>() ?? []];
            set => Images = [.. value?.Cast<Image>() ?? []];
        }
    }
}
