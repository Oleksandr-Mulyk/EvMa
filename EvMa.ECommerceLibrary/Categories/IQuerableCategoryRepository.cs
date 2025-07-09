using EvMa.Core;

namespace EvMa.ECommerceLibrary.Categories
{
    public interface IQuerableCategoryRepository : IRepository<ICategory>, IQuerableRepository<ICategory>
    {
        public IQueryable<ICategory> GetAllByParentCategoryId(Guid parentCategoryId);
    }
}
