using System.ComponentModel.DataAnnotations.Schema;

namespace EvMa.CatalogService.Data
{
    public class Category : ICategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column("ParentCategory")]
        private Category? _parentCategory;

        [NotMapped]
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

        [Column("ChildCategories")]
        private IList<Category>? _childCategories;

        [NotMapped]
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

        [Column("Products")]
        private IList<Product> _products = [];

        [NotMapped]
        public IList<IProduct> Products
        {
            get => (IList<IProduct>)_products;
            set => value.Cast<Product>().ToList();
        }

        [Column("Images")]
        private IList<Image>? _images = [];

        [NotMapped]
        public IList<IImage>? Images
        {
            get => (IList<IImage>?)_images;
            set => _images = value?.Cast<Image>().ToList();
        }
    }
}
