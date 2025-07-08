namespace EvMa.CatalogService.Data.Repositories
{
    public interface IRepository<T, TId> where T : class
    {
        public Task<T> GetByIdAsync(TId id);

        public IQueryable<T> GetAll();

        public Task<T> AddAsync(T entity);

        public Task<T> UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }

    public interface IRepository<T> : IRepository<T, Guid> where T : class { }
}
    