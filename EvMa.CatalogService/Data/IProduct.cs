namespace EvMa.CatalogService.Data
{
    public interface IProduct<TAttribute, TAttributeSet, TAttributeValue, TPrice, TImage>
        where TAttribute : IProductAttribute
        where TAttributeSet : IAttributeSet<TAttribute>
        where TAttributeValue : IAttributeValue<TAttribute>
        where TPrice : IPrice
        where TImage : IImage
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public (decimal Length, decimal Width, decimal Height) Dimensions { get; set; }

        public decimal RegularPrice { get; set; }

        public IList<TPrice>? Prices { get; set; }

        public decimal StockQuantity { get; set; }

        public TAttributeSet AttributeSet { get; set; }

        public IList<TAttributeValue> AttributeValues { get; set; }

        public IList<TImage>? Images { get; set; }

        public IList<string>? Tags { get; set; }

        public bool IsActive { get; set; }
    }
}
