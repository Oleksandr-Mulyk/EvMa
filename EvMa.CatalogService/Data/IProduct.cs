namespace EvMa.CatalogService.Data
{
    public interface IProduct
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public (decimal Length, decimal Width, decimal Height) Dimensions { get; set; }

        public decimal RegularPrice { get; set; }

        public IList<IPrice>? Prices { get; set; }

        public decimal StockQuantity { get; set; }

        public IAttributeSet AttributeSet { get; set; }

        public IList<IAttributeValue> AttributeValues { get; set; }

        public ICategory Category { get; set; }

        public IList<IImage>? Images { get; set; }

        public IList<string>? Tags { get; set; }

        public bool IsActive { get; set; }
    }
}
