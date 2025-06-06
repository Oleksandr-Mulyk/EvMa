namespace EvMa.CatalogService.Data
{
    public interface ICategory<TProduct, TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage>
        where TProduct : IProduct<TProductAttribute, TAttributeSet, TAttributeValue, TPrice, TImage>
        where TProductAttribute : IProductAttribute
        where TAttributeSet : IAttributeSet<TProductAttribute>
        where TAttributeValue : IAttributeValue<TProductAttribute>
        where TPrice : IPrice
        where TImage : IImage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public IList<int>? ChildCategoryIds { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IList<TProduct> Products { get; set; }

        public IList<TImage>? Images { get; set; }
    }

    public interface ICategory :
        ICategory<IProduct, IProductAttribute, IAttributeSet, IAttributeValue, IPrice, IImage> { }
}
