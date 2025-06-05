using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data
{
    public class AttributeValue : IAttributeValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("Attribute")]
        private Attribute _attribute { get; set; } = new();

        [NotMapped]
        public IAttribute Attribute
        {
            get => _attribute;
            set => _attribute = (Attribute)value;
        }

        [Column("Product")]
        private Product _product { get; set; } = new();

        [NotMapped]
        public IProduct Product
        {
            get => _product;
            set => _product = (Product)value;
        }

        public string Value { get; set; } = string.Empty;
    }
}
