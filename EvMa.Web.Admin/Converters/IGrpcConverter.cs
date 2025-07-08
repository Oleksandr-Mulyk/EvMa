namespace EvMa.Web.Admin.Converters
{
    public interface IGrpcConverter<TGrpc, TEntity>
    {
        TGrpc ConvertToEntity(TEntity source);

        TEntity ConvertToGrpc(TGrpc destination);
    }
}
