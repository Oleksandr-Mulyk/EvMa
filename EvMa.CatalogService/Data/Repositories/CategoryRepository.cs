using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class CategoryRepository(ApplicationContext dbContext) :
        DbContextRepository<ICategory, Category>(dbContext), ICategoryRepository
    {
        protected override DbSet<Category> DbSet => dbContext.Categories;

        public override async Task<ICategory> GetByIdAsync(Guid id) =>
            await DbSet
            .Include(category => category.Images)
            .Include(category => category.Products)
            .ThenInclude(product => product.Prices)
            .Include(category => category.Products)
            .ThenInclude(product => product.AttributeSet)
            .ThenInclude(attributeSet => attributeSet.Attributes)
            .Include(category => category.Products)
            .ThenInclude(product => product.AttributeValues)
            .ThenInclude(attributeValue => attributeValue.Attribute)
            .Include(category => category.Products)
            .ThenInclude(product => product.Images)
            .FirstOrDefaultAsync(category => category.Id == id) ?? throw new Exception(NotFoundMessage);


        public override IQueryable<ICategory> GetAll() =>
            DbSet
                .Include(category => category.Images)
                .Include(category => category.Products)
                .AsQueryable()
                .Select(category => new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    IsActive = category.IsActive,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    Images = category.Images,
                    ProductIds = category.Products.Select(p => p.Id).ToList()
                } as ICategory);

        public virtual IQueryable<ICategory> GetAllByParentCategoryId(Guid parentCategoryId) =>
            DbSet
                .Include(category => category.Images)
                .Include(category => category.Products)
                .AsQueryable()
                .Where(category => category.ParentCategoryId == parentCategoryId)
                .Select(category => new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    IsActive = category.IsActive,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    Images = category.Images,
                    ProductIds = category.Products.Select(p => p.Id).ToList()
                } as ICategory);

        public override async Task<ICategory> UpdateAsync(ICategory entity)
        {
            var category = DbSet
                .Include(c => c.Images)
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == entity.Id) ?? throw new Exception(NotFoundMessage);

            category.Name = entity.Name;
            category.Description = entity.Description;
            category.ParentCategoryId = entity.ParentCategoryId;
            category.IsActive = entity.IsActive;
            category.CreatedAt = entity.CreatedAt;
            category.UpdatedAt = entity.UpdatedAt;

            entity.Images.Clear();
            foreach (var image in entity.Images.Cast<Image>())
            {
                var trackedImage = dbContext.Images.Local.FirstOrDefault(i => i.Id == image.Id);
                if (trackedImage == null)
                {
                    dbContext.Images.Attach(image);
                    trackedImage = image;
                }
                category.Images.Add(trackedImage);
            }

            category.Products = [.. entity.Products.Cast<Product>()];

            return await base.UpdateAsync(category);
        }
    }
}
