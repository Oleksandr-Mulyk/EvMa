using EvMa.ECommerceLibrary.Grpc.Converters;
using EvMa.ECommerceLibrary.Grpc.Protos;
using EvMa.ECommerceLibrary.Products;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Services
{
    public class ProductGrpc(
        IQuerableProductRepository productRepository,
        IGrpcConverter<GrpcProduct, IProduct> grpcProductConverter
        ) : ProductService.ProductServiceBase
    {
        public override async Task<GrpcProduct> GetById(ProductIdRequest idRequest, ServerCallContext context)
        {
            IProduct product = await productRepository.GetByIdAsync(Guid.Parse(idRequest.Id));

            return grpcProductConverter.ConvertToGrpc(product);
        }

        public override async Task<GrpcProduct> GetBySku(SkuRequest skuRequest, ServerCallContext context)
        {
            IProduct product = await productRepository.GetBySkuAsync(skuRequest.Sku);

            return grpcProductConverter.ConvertToGrpc(product);
        }

        public override async Task<ProductListResponse> GetAll(Empty request, ServerCallContext context)
        {
            List<IProduct> products = await productRepository.GetAll().ToListAsync();

            return new ProductListResponse { Products = { products.Select(grpcProductConverter.ConvertToGrpc) } };
        }

        public override async Task<GrpcProduct> Add(GrpcProduct grpcProduct, ServerCallContext context)
        {
            IProduct product = grpcProductConverter.ConvertToEntity(grpcProduct);
            product = await productRepository.AddAsync(product);

            return grpcProductConverter.ConvertToGrpc(product);
        }

        public override async Task<GrpcProduct> Update(GrpcProduct grpcProduct, ServerCallContext context)
        {
            IProduct product = grpcProductConverter.ConvertToEntity(grpcProduct);
            product = await productRepository.UpdateAsync(product);

            return grpcProductConverter.ConvertToGrpc(product);
        }

        public override async Task<Empty> DeleteById(ProductIdRequest idRequest, ServerCallContext context)
        {
            IProduct product = await productRepository.GetByIdAsync(Guid.Parse(idRequest.Id));
            await productRepository.DeleteAsync(product);

            return new Empty();
        }
    }
}
