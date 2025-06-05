using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Data.Repositories
{
    public class DbContextRepository<T, TClass, TId>(DbSet<TClass> dbSet, ApplicationContext dbContext)
        : IRepository<T, TId> where T : class where TClass : class, T
    {
        public virtual async Task<T?> GetByIdAsync(TId id) =>
            await dbSet.FindAsync(id) ?? throw new Exception(NotFoundMessage);

        public virtual IQueryable<T> GetAll() => dbSet.AsQueryable().Select(item => item as T);

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await dbSet.AddAsync(entity as TClass ?? throw new InvalidCastException());

            if (result.State == EntityState.Added)
            {
                await dbContext.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception(NotAddedMessage);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var result = dbSet.Update(entity as TClass ?? throw new InvalidCastException());

            if (result.State == EntityState.Modified)
            {
                await dbContext.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception(NotUpdatedMessage);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            var result = dbSet.Remove(entity as TClass ?? throw new InvalidCastException());

            if (result.State == EntityState.Deleted)
            {
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception(NotDeletedMessage);
            }
        }

        protected virtual string NotFoundMessage => $"{typeof(TClass).Name} not found";

        protected virtual string NotAddedMessage => $"{typeof(TClass).Name} not added";

        protected virtual string NotUpdatedMessage => $"{typeof(TClass).Name} not updated";

        protected virtual string NotDeletedMessage => $"{typeof(TClass).Name} not deleted";
    }

    public class DbContextRepository<T, TClass>(DbSet<TClass> dbSet, ApplicationContext dbContext) :
        DbContextRepository<T, TClass, Guid>(dbSet, dbContext), IRepository<T>
        where T : class where TClass : class, T { }
}
