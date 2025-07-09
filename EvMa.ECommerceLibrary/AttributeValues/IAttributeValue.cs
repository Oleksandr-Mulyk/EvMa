using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.ECommerceLibrary.AttributeValues
{
    public interface IAttributeValue<TProductAttribute> where TProductAttribute : IProductAttribute
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public TProductAttribute Attribute { get; set; }
    }

    public interface IAttributeValue : IAttributeValue<IProductAttribute> { }
}
