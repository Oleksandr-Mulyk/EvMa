using EvMa.Core;
using EvMa.Web.Admin.Converters;

namespace EvMa.Web.Admin.Repositories
{
    public abstract class GrpcRepository<T, TRequest, TId, TIdRequest, TClient, TConverter>(
        TClient client,
        TConverter converter
        ) : IRepository<T, TId>
            where T : class
            where TRequest : class, new()
            where TId : notnull
            where TIdRequest : class, new()
            where TClient : class
            where TConverter : class, IGrpcConverter<T, TRequest>
    {
        public virtual async Task<T> GetByIdAsync(TId id)
        {
            var request = ToIdRequest(id);
            var result = await (client.GetType().GetMethod(GetByIdMethodName)?.Invoke(client, [request]) as Task<TRequest>);

            return result != null ?
                converter.ConvertToEntity(result) :
                throw new InvalidOperationException($"Method '{GetByIdMethodName}' returned null for id '{id}'.");
        }

        public virtual IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IList<T>> GetListAsync()
        {
            var result = await (client.GetType().GetMethod(GetAllMethodName)?.Invoke(client, []) as Task<IList<TRequest>>);

            return result != null ?
                result.Select(converter.ConvertToEntity).ToList() :
                throw new InvalidOperationException($"Method '{GetAllMethodName}' returned null.");
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var request = converter.ConvertToGrpc(entity);
            var result = await (client.GetType().GetMethod(AddMethodName)?.Invoke(client, [request]) as Task<TRequest>);

            return result != null ?
                converter.ConvertToEntity(result) :
                throw new InvalidOperationException($"Method '{AddMethodName}' returned null for entity '{entity}'.");
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var request = converter.ConvertToGrpc(entity);
            var result = await (client.GetType().GetMethod(UpdateMethodName)?.Invoke(client, [request]) as Task<TRequest>);

            return result != null ?
                converter.ConvertToEntity(result) :
                throw new InvalidOperationException($"Method '{UpdateMethodName}' returned null for entity '{entity}'.");
        }

        public virtual async Task DeleteAsync(T entity)
        {
            var id = entity.GetType().GetProperty(IdPropertyName)?.GetValue(entity);
            var request = ToIdRequest((TId)id);
            await (client.GetType().GetMethod(DeleteByIdMethodName)?.Invoke(client, [request]) as Task);
        }

        protected virtual TIdRequest ToIdRequest(TId id)
        {
            var request = new TIdRequest();
            var idProperty = typeof(TIdRequest).GetProperty(GrpcIdPropertyName);
            if (idProperty != null)
            {
                idProperty.SetValue(request, id.ToString());
            }
            else
            {
                throw new InvalidOperationException($"Request does not contain a property named '{GrpcIdPropertyName}'.");
            }
            return request;
        }

        protected virtual string GetByIdMethodName => "GetById";

        protected virtual string GetAllMethodName => "GetAll";

        protected virtual string AddMethodName => "Add";

        protected virtual string UpdateMethodName => "Update";

        protected virtual string DeleteByIdMethodName => "DeleteById";

        protected virtual string IdPropertyName => "Id";

        protected virtual string GrpcIdPropertyName => "Id";
    }
}
