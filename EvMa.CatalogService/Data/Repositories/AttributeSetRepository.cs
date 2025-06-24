using EvMa.CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class AttributeSetRepository(ApplicationContext applicationContext) : 
        DbContextRepository<IAttributeSet, AttributeSet>(applicationContext)
    {
        protected override DbSet<AttributeSet> DbSet => applicationContext.AttributeSets;

        public override IQueryable<IAttributeSet> GetAll() =>
            applicationContext.AttributeSets
                .Include(aset => aset.Attributes)
                .AsQueryable()
                .Select(aset => aset as IAttributeSet);

        public override async Task<IAttributeSet> GetByIdAsync(Guid id) =>
            await applicationContext.AttributeSets
                .Include(aset => aset.Attributes)
                .FirstOrDefaultAsync(aset => aset.Id == id) 
            ?? throw new Exception(NotFoundMessage);

        public override async Task<IAttributeSet> UpdateAsync(IAttributeSet entity)
        {
            _ = await GetByIdAsync(entity.Id);
            //IAttributeSet? oldEntity = entity;
            await applicationContext.SaveChangesAsync();
            return entity;
        }
    }
}
