namespace EvMa.Core
{
    public interface IListableRepository<T>
    {
        public Task<IList<T>> GetListAsync();
    }
}
