namespace EvMa.ECommerceLibrary.Models
{
    public class Image : IImage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Url { get; set; } = string.Empty;

        public string AltText { get; set; } = string.Empty;

        public int Order { get; set; } = 0;
    }
}
