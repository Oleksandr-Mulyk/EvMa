namespace EvMa.ECommerceLibrary.Grpc.Converters
{
    public interface IGrpcConverter<TGrpc, TEntity>
    {
        TEntity ConvertToEntity(TGrpc source);

        TGrpc ConvertToGrpc(TEntity entity);
    }
}
