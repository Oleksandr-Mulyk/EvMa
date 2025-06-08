using EvMa.CatalogService.Data;
using EvMa.CatalogService.Data.Repositories;
using EvMa.CatalogService.Protos;
using EvMa.CatalogService.Services.Converters;
using EvMa.CatalogService.Services.Extensions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Services
{
    public class ProductGrpc(IProductRepository productRepository, IGprcProductConverter gprcProductConverter)
        : ProductService.ProductServiceBase
    {
        public override async Task<GrpcProduct> GetById(IdRequest idRequest, ServerCallContext context)
        {
            IProduct product = await productRepository.GetByIdAsync(Guid.Parse(idRequest.Id));

            return product.ToGrpcProduct();
        }

        public override async Task<GrpcProduct> GetBySku(SkuRequest skuRequest, ServerCallContext context)
        {
            IProduct product = await productRepository.GetBySkuAsync(skuRequest.Sku);

            return product.ToGrpcProduct();
        }

        public override async Task<ProductListResponse> GetAll(Empty request, ServerCallContext context)
        {
            List<IProduct> products = await productRepository.GetAll().ToListAsync();

            return new ProductListResponse
            {
                Products = { products.Select(p => p.ToGrpcProduct()) }
            };
        }

        public override async Task<GrpcProduct> Add(GrpcProduct grpcProduct, ServerCallContext context)
        {
            IProduct product = gprcProductConverter.ToProduct(grpcProduct);
            product = await productRepository.AddAsync(product);

            return product.ToGrpcProduct();
        }

        public override async Task<GrpcProduct> Update(GrpcProduct grpcProduct, ServerCallContext context)
        {
            IProduct product = gprcProductConverter.ToProduct(grpcProduct);
            product = await productRepository.UpdateAsync(product);

            return product.ToGrpcProduct();
        }

        public override async Task<Empty> DeleteById(IdRequest idRequest, ServerCallContext context)
        {
            IProduct product = await productRepository.GetByIdAsync(Guid.Parse(idRequest.Id));
            await productRepository.DeleteAsync(product);

            return new Empty();
        }
    }
}
