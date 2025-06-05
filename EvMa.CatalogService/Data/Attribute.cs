namespace EvMa.CatalogService.Data
{
    public class Attribute : IAttribute
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = typeof(string).Name;
    }
}
