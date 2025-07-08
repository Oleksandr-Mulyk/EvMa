using EvMa.Web.Admin.Protos;

namespace EvMa.Web.Admin.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, WebApplicationBuilder? builder)
        {
            services.AddGrpcClient<CategoryService.CategoryServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["evma-catalogservice"]);
            });

            services.AddGrpcClient<AttributeSetService.AttributeSetServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["evma-catalogservice"]);
            });

            services.AddGrpcClient<ProductService.ProductServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["evma-catalogservice"]);
            });

            return services;
        }
    }
}
