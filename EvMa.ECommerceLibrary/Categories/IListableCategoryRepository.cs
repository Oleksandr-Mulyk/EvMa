using EvMa.Core;

namespace EvMa.ECommerceLibrary.Categories
{
    public interface IListableCategoryRepository : IRepository<ICategory>, IListableRepository<ICategory>
    {
        public Task<IList<ICategory>> GetListByParentCategoryId(Guid parentCategoryId);
    }
}
