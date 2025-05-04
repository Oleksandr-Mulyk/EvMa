namespace EvMa.CatalogService.Data
{
    public interface ICategory
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICategory? ParentCategory { get; set; }

        public IList<ICategory>? ChildCategories { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IList<object> Products { get; set; }

        public IList<IImage> Images { get; set; }
    }
}
