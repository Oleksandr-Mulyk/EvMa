namespace EvMa.CatalogService.Data
{
    public interface IAttributeValue
    {
        public Guid AttributeValueId { get; set; }

        public IAttribute Attribute { get; set; }

        public object Product { get; set; }

        public string Value { get; set; }
    }
}
