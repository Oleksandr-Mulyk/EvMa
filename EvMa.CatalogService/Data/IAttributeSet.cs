namespace EvMa.CatalogService.Data
{
    public interface IAttributeSet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<IAttribute> Attributes { get; set; }
    }
}
