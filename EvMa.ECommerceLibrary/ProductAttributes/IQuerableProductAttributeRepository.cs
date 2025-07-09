using EvMa.Core;

namespace EvMa.ECommerceLibrary.ProductAttributes
{
    public interface IQuerableProductAttributeRepository
        : IRepository<IProductAttribute>, IQuerableRepository<IProductAttribute>
    { }
}
