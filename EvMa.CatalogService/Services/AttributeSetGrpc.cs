using EvMa.CatalogService.Data.Repositories;
using EvMa.ECommerceLibrary.AttributeSets;
using EvMa.ECommerceLibrary.Grpc.Converters;
using EvMa.ECommerceLibrary.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Services
{
    public class AttributeSetGrpc(
        AttributeSetRepository attributeSetRepository,
        IGrpcConverter<GrpcAttributeSet, IAttributeSet> grpcAttributeSetConverter
        ) : AttributeSetService.AttributeSetServiceBase
    {
        public override async Task<GrpcAttributeSet> GetById(AttributeSetIdRequest idRequest, ServerCallContext context)
        {
            IAttributeSet attributeSet = await attributeSetRepository.GetByIdAsync(Guid.Parse(idRequest.Id));

            return grpcAttributeSetConverter.ConvertToGrpc(attributeSet);
        }

        public override async Task<AttributeSetListResponse> GetAll(Empty request, ServerCallContext context)
        {
            List<IAttributeSet> attributeSets = await attributeSetRepository.GetAll().ToListAsync();

            return new AttributeSetListResponse
            {
                AttributeSets = { attributeSets.Select(grpcAttributeSetConverter.ConvertToGrpc) }
            };
        }
        public override async Task<GrpcAttributeSet> Add(GrpcAttributeSet grpcAttributeSet, ServerCallContext context)
        {
            IAttributeSet attributeSet = grpcAttributeSetConverter.ConvertToEntity(grpcAttributeSet);
            attributeSet = await attributeSetRepository.AddAsync(attributeSet);

            return grpcAttributeSetConverter.ConvertToGrpc(attributeSet);
        }
        public override async Task<GrpcAttributeSet> Update(GrpcAttributeSet grpcAttributeSet, ServerCallContext context)
        {
            IAttributeSet attributeSet = grpcAttributeSetConverter.ConvertToEntity(grpcAttributeSet);
            attributeSet = await attributeSetRepository.UpdateAsync(attributeSet);

            return grpcAttributeSetConverter.ConvertToGrpc(attributeSet);
        }
        public override async Task<Empty> DeleteById(AttributeSetIdRequest idRequest, ServerCallContext context)
        {
            IAttributeSet attributeSet = await attributeSetRepository.GetByIdAsync(Guid.Parse(idRequest.Id));
            await attributeSetRepository.DeleteAsync(attributeSet);

            return new Empty();
        }
    }
}
