using EvMa.CatalogService.Data.Repositories;
using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.AttributeValues;
using EvMa.ECommerceLibrary.Categories;
using EvMa.ECommerceLibrary.Grpc.Converters;
using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.Images;
using EvMa.ECommerceLibrary.Prices;
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
            services.AddTransient<IGrpcConverter<GrpcImage, IImage>, GrpcImageConverter>();
            services.AddTransient<IGrpcConverter<GrpcPrice, IPrice>, GrpcPriceConverter>();
            services.AddTransient<IGrpcConverter<GrpcProductAttribute, IProductAttribute>, GrpcProductAttributeConverter>();
            services.AddTransient<IGrpcConverter<GrpcAttributeValue, IAttributeValue>, GrpcAttributeValueConverter>();
            services.AddTransient<IGrpcConverter<GrpcAttributeSet, IAttributeSet>, GrpcAttributeSetConverter>();
            services.AddTransient<IGrpcConverter<GrpcProduct, IProduct>, GprcProductConverter>();
            services.AddTransient<IGrpcConverter<GrpcCategory, ICategory>, GrpcCategoryConverter>();

            return services;
        }
    }
}
