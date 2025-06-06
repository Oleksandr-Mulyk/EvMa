using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class CategoryRepository(DbSet<Category> dbSet, ApplicationContext dbContext) :
        DbContextRepository<ICategory, Category>(dbSet, dbContext), ICategoryRepository
    {
        public override IQueryable<ICategory> GetAll() =>
            base.GetAll()
                .Include(category => category.Products)
                .Include(category => category.Images);

        public virtual IQueryable<ICategory> GetAllByParentCategoryId(int parentCategoryId) =>
            GetAll().Where(category => category.ParentCategoryId == parentCategoryId);
    }
}
