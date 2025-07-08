namespace EvMa.ECommerceLibrary.Models
{
    public class AttributeValue<TProductAttribute> : IAttributeValue<TProductAttribute>
        where TProductAttribute : IProductAttribute
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Value { get; set; } = string.Empty;

        public TProductAttribute Attribute { get; set; }
    }

    public class AttributeValue : AttributeValue<ProductAttribute>, IAttributeValue
    {
        IProductAttribute IAttributeValue<IProductAttribute>.Attribute
        {
            get => Attribute;
            set => Attribute = value as ProductAttribute ?? throw new InvalidCastException("Invalid attribute type.");
        }
    }
}
