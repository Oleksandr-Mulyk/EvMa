using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class CategoryRepository(ApplicationContext dbContext) :
        DbContextRepository<ICategory, Category>(dbContext), ICategoryRepository
    {
        protected override DbSet<Category> DbSet => dbContext.Categories;

        public override IQueryable<ICategory> GetAll() =>
            base.GetAll()
                .Include(category => category.Products)
                .Include(category => category.Images);

        public virtual IQueryable<ICategory> GetAllByParentCategoryId(Guid parentCategoryId) =>
            GetAll().Where(category => category.ParentCategoryId == parentCategoryId);
    }
}
