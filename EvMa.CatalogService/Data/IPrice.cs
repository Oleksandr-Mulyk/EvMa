namespace EvMa.CatalogService.Data
{
    public interface IPrice
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public decimal? MinQuantity { get; set; }

        public decimal? MaxQuantity { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
