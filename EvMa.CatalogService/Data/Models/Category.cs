using EvMa.ECommerceLibrary.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data.Models
{
    public class Category<TProduct, TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage> :
        ICategory<TProduct, TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage>
        where TProduct : IProduct<TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage>
        where TProductAttribute : IProductAttribute
        where TAttributeSet : IAttributeSet<TProductAttribute>
        where TAttributeValue : IAttributeValue<TProductAttribute>
        where TPrice : IPrice
        where TImage : IImage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid? ParentCategoryId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public IList<TProduct> Products { get; set; } = [];

        public IList<TImage>? Images { get; set; } = [];

        [NotMapped]
        public virtual IList<Guid> ProductIds { get; set; } = [];

    }

    public class Category : Category<Product, ProductAttribute, AttributeSet, AttributeValue, Price, Image>, ICategory
    {
        IList<IProduct> ICategory<IProduct, IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage>.Products
        {
            get => [.. Products?.Cast<IProduct>() ?? []];
            set => Products = [.. value?.Cast<Product>() ?? []];
        }

        IList<IImage>? ICategory<IProduct, IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage>.Images
        {
            get => [.. Images?.Cast<IImage>() ?? []];
            set => Images = [.. value?.Cast<Image>() ?? []];
        }
    }
}
