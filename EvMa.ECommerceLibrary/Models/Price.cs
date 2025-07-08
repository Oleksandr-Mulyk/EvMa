namespace EvMa.ECommerceLibrary.Models
{
    public class Price : IPrice
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public decimal Value { get; set; }

        public decimal? MinQuantity { get; set; }

        public decimal? MaxQuantity { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
