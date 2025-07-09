using EvMa.ECommerceLibrary.AttributeSets;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class AttributeSetRepository(ApplicationContext dbContext) : 
        DbContextRepository<IAttributeSet, AttributeSet>(dbContext), IQuerableAttributeSetRepository
    {
        protected override DbSet<AttributeSet> DbSet => dbContext.AttributeSets;

        public override IQueryable<IAttributeSet> GetAll() =>
            DbSet
            .Include(aset => aset.Attributes)
            .AsQueryable()
            .Select(aset => aset as IAttributeSet);

        public override async Task<IAttributeSet> GetByIdAsync(Guid id) =>
            await DbSet
                .Include(aset => aset.Attributes)
                .FirstOrDefaultAsync(aset => aset.Id == id) 
            ?? throw new Exception(NotFoundMessage);

        public override async Task<IAttributeSet> UpdateAsync(IAttributeSet entity)
        {
            var attributeSet = await GetByIdAsync(entity.Id);
            attributeSet.Name = entity.Name;
            attributeSet.Attributes = entity.Attributes;
            await dbContext.SaveChangesAsync();

            return attributeSet;
        }
    }
}
