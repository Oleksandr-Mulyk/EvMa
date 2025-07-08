using EvMa.CatalogService.Data.Models;
using EvMa.CatalogService.Data.Repositories;
using EvMa.CatalogService.Services.Converters;
using EvMa.Core;
using EvMa.ECommerceLibrary.Models;
using EvMa.ECommerceLibrary.Repositories;

namespace EvMa.CatalogService.Extensions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRepository<IAttributeSet>, AttributeSetRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IRepository<IProductAttribute>, ProductAttributeRepository>();

            return services;
        }

        public static IServiceCollection AddServiceGrpcModelsToModelsConverters(this IServiceCollection services)
        {
            services.AddTransient<IGrpcImageConverter, GrpcImageConverter>();
            services.AddTransient<IGrpcPriceConverter, GrpcPriceConverter>();
            services.AddTransient<IGrpcProductAttributeConverter, GrpcProductAttributeConverter>();
            services.AddTransient<IGrpcAttributeValueConverter, GrpcAttributeValueConverter>();
            services.AddTransient<IGrpcAttributeSetConverter, GrpcAttributeSetConverter>();
            services.AddTransient<IGprcProductConverter, GprcProductConverter>();
            services.AddTransient<IGrpcCategoryConverter, GrpcCategoryConverter>();

            return services;
        }
    }
}
