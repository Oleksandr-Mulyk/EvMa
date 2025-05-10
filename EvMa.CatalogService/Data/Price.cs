using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data
{
    public class Price : IPrice
    {
        public Guid PriceId { get; set; } = Guid.NewGuid();

        [Column("Product")]
        private Product _product { get; set; } = new Product();

        [NotMapped]
        public IProduct Product
        {
            get => _product;
            set => _product = (Product)value;
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MaxQuantity { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
