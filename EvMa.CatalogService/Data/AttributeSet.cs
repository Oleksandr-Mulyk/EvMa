using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data
{
    public class AttributeSet : IAttributeSet
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        [Column("Attributes")]
        private IList<Attribute> _attributes = [];

        [NotMapped]
        public IList<IAttribute> Attributes
        {
            get => (IList<IAttribute>)_attributes;
            set => _attributes = [.. value.Cast<Attribute>()];
        }
    }
}
