using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class AttributeSetRepository(DbSet<AttributeSet> dbSet, ApplicationContext applicationContext) : 
        DbContextRepository<IAttributeSet, AttributeSet>(dbSet, applicationContext)
    {
        public override IQueryable<IAttributeSet> GetAll() =>
            base.GetAll().Include(aset => aset.Attributes);
    }
}
