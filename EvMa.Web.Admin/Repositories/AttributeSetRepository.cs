using EvMa.Core;
using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.Web.Admin.Protos;

namespace EvMa.Web.Admin.Repositories
{
    public class AttributeSetRepository(AttributeSetService.AttributeSetServiceClient client)
        : IRepository<IAttributeSet>
    {
        public async Task<IAttributeSet> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IAttributeSet> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IAttributeSet> AddAsync(IAttributeSet entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IAttributeSet> UpdateAsync(IAttributeSet entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(IAttributeSet entity)
        {
            throw new NotImplementedException();
        }
    }
}
