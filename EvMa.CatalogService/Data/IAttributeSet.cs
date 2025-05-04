namespace EvMa.CatalogService.Data
{
    public interface IAttributeSet
    {
        public Guid AttributeSetId { get; set; }

        public string Name { get; set; }

        public IList<IAttribute> Attributes { get; set; }
    }
}
