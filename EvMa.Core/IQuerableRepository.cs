namespace EvMa.Core
{
    public interface IQuerableRepository<T>
    {
        public IQueryable<T> GetAll();
    }
}
