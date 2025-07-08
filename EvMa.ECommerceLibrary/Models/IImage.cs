namespace EvMa.ECommerceLibrary.Models
{
    public interface IImage
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string AltText { get; set; }

        public int Order { get; set; }
    }
}
