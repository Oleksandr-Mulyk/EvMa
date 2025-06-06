namespace EvMa.CatalogService.Data.Models
{
    public class AttributeSet<TProductAttribute> : IAttributeSet<TProductAttribute>
        where TProductAttribute : IProductAttribute
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public IList<TProductAttribute> Attributes{ get; set; }
    }

    public class AttributeSet : AttributeSet<ProductAttribute> { }
}
