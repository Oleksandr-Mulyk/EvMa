namespace EvMa.CatalogService.Data
{
    public class AttributeSet : IAttributeSet
    {
        public Guid AttributeSetId { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        private IList<Attribute> _attributes = [];

        IList<IAttribute> IAttributeSet.Attributes
        {
            get => (IList<IAttribute>)_attributes;
            set => _attributes = [.. value.Cast<Attribute>()];
        }
    }
}
