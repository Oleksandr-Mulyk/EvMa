namespace EvMa.CatalogService.Data
{
    public class Category : ICategory
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        private Category? _parentCategory;

        public ICategory? ParentCategory
        {
            get => _parentCategory;
            set
            {
                _parentCategory = value as Category;
                if (_parentCategory != null && !_parentCategory.ChildCategories!.Contains(this))
                {
                    _parentCategory.ChildCategories!.Add(this);
                }
            }
        }

        private IList<Category>? _childCategories;

        public IList<ICategory>? ChildCategories
        {
            get => _childCategories?.Cast<ICategory>().ToList();
            set
            {
                _childCategories = value?.Cast<Category>().ToList();
                if (_childCategories != null)
                {
                    foreach (var child in _childCategories)
                    {
                        if (child.ParentCategory != this)
                        {
                            child.ParentCategory = this;
                        }
                    }
                }
            }
        }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public IList<object> Products { get; set; } = [];

        public IList<IImage> Images { get; set; } = [];
    }
}
