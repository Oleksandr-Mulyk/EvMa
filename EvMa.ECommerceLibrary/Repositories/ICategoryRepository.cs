using EvMa.Core;
using EvMa.ECommerceLibrary.Models;

namespace EvMa.ECommerceLibrary.Repositories
{
    public interface ICategoryRepository : IRepository<ICategory>
    {
        public IQueryable<ICategory> GetAllByParentCategoryId(Guid parentCategoryId);
    }
}
