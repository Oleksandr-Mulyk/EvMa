using EvMa.ECommerceLibrary.Models;

namespace EvMa.CatalogService.Data.Repositories
{
    public interface ICategoryRepository : IRepository<ICategory>
    {
        public IQueryable<ICategory> GetAllByParentCategoryId(Guid parentCategoryId);
    }
}
