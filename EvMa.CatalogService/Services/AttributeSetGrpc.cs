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
    public class AttributeSetGrpc(
        IRepository<IAttributeSet> attributeSetRepository,
        IGrpcAttributeSetConverter grpcAttributeSetConverter
        ) : AttributeSetService.AttributeSetServiceBase
    {
        public override async Task<GrpcAttributeSet> GetById(AttributeSetIdRequest idRequest, ServerCallContext context)
        {
            IAttributeSet attributeSet = await attributeSetRepository.GetByIdAsync(Guid.Parse(idRequest.Id));

            return attributeSet.ToGrpcAttributeSet();
        }

        public override async Task<AttributeSetListResponse> GetAll(Empty request, ServerCallContext context)
        {
            List<IAttributeSet> attributeSets = await attributeSetRepository.GetAll().ToListAsync();

            return new AttributeSetListResponse
            {
                AttributeSets = { attributeSets.Select(aset => aset.ToGrpcAttributeSet()) }
            };
        }
        public override async Task<GrpcAttributeSet> Add(GrpcAttributeSet grpcAttributeSet, ServerCallContext context)
        {
            IAttributeSet attributeSet = grpcAttributeSetConverter.ToAttributeSet(grpcAttributeSet);
            attributeSet = await attributeSetRepository.AddAsync(attributeSet);

            return attributeSet.ToGrpcAttributeSet();
        }
        public override async Task<GrpcAttributeSet> Update(GrpcAttributeSet grpcAttributeSet, ServerCallContext context)
        {
            IAttributeSet attributeSet = grpcAttributeSetConverter.ToAttributeSet(grpcAttributeSet);
            attributeSet = await attributeSetRepository.UpdateAsync(attributeSet);

            return attributeSet.ToGrpcAttributeSet();
        }
        public override async Task<Empty> DeleteById(AttributeSetIdRequest idRequest, ServerCallContext context)
        {
            IAttributeSet attributeSet = await attributeSetRepository.GetByIdAsync(Guid.Parse(idRequest.Id));
            await attributeSetRepository.DeleteAsync(attributeSet);

            return new Empty();
        }
    }
}
