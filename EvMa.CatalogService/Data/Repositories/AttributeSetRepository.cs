using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class AttributeSetRepository(ApplicationContext applicationContext) : 
        DbContextRepository<IAttributeSet, AttributeSet>(applicationContext)
    {
        protected override DbSet<AttributeSet> DbSet => applicationContext.AttributeSets;

        public override IQueryable<IAttributeSet> GetAll() =>
            base.GetAll().Include(aset => aset.Attributes);
    }
}
