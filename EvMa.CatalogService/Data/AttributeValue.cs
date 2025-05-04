namespace EvMa.CatalogService.Data
{
    public class AttributeValue : IAttributeValue
    {
        public Guid AttributeValueId { get; set; } = Guid.NewGuid();

        private Attribute _attribute { get; set; } = new Attribute();

        public IAttribute Attribute
        {
            get => _attribute;
            set => _attribute = (Attribute)value;
        }

        public object Product { get; set; } = new object();

        public string Value { get; set; } = string.Empty;
    }
}
