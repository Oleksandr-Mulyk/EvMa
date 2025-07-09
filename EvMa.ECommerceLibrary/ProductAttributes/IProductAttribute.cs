namespace EvMa.ECommerceLibrary.ProductAttributes
{
    public interface IProductAttribute
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
