using EvMa.CatalogService.Data.Repositories;
using EvMa.CatalogService.Services.Converters;
using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.Categories;
using EvMa.ECommerceLibrary.ProductAttributes;
using EvMa.ECommerceLibrary.Products;

namespace EvMa.CatalogService.Extensions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IQuerableProductRepository, ProductRepository>();
            services.AddTransient<IQuerableAttributeSetRepository, AttributeSetRepository>();
            services.AddTransient<IQuerableCategoryRepository, CategoryRepository>();
            services.AddTransient<IQuerableProductAttributeRepository, ProductAttributeRepository>();

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
