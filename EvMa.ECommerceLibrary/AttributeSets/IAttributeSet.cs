using EvMa.ECommerceLibrary.ProductAttributes;

namespace EvMa.ECommerceLibrary.AttributeSets
{
    public interface IAttributeSet<TProductAttribute> where TProductAttribute : IProductAttribute
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<TProductAttribute> Attributes { get; set; }
    }

    public interface IAttributeSet : IAttributeSet<IProductAttribute> { }
}
