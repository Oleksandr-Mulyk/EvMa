namespace EvMa.CatalogService.Data
{
    public interface IImage
    {
        public Guid ImageId { get; set; }

        public string Url { get; set; }

        public string AltText { get; set; }

        public int Order { get; set; }
    }
}
