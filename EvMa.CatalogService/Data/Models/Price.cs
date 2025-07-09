using EvMa.ECommerceLibrary.Prices;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data.Models
{
    public class Price : IPrice
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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
