namespace EvMa.CatalogService.Data
{
    public interface IAttribute
    {
        public Guid AttributeId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
