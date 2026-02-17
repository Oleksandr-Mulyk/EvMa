using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace EvMa.ServiceDefaults.Interceptors
{
    public class GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger) : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "gRPC Error occurred during {Method}", context.Method);

                var status = ex switch
                {
                    ArgumentException => new Status(StatusCode.InvalidArgument, ex.Message),
                    _ => new Status(StatusCode.Internal, "An internal server error occurred.")
                };

                throw new RpcException(status);
            }
        }
    }
}
