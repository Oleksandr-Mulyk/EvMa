using EvMa.ECommerceLibrary.Categories;
using EvMa.ECommerceLibrary.Grpc.Converters;
using EvMa.ECommerceLibrary.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EvMa.CatalogService.Services
{
    public class CategoryGrpc(
        IQuerableCategoryRepository categoryRepository,
        IGrpcConverter<GrpcCategory, ICategory> grpcCategoryConverter
        ) : CategoryService.CategoryServiceBase
    {
        public override async Task<GrpcCategory> GetById(CategoryIdRequest idRequest, ServerCallContext context)
        {
            ICategory category = await categoryRepository.GetByIdAsync(Guid.Parse(idRequest.Id));

            return grpcCategoryConverter.ConvertToGrpc(category);
        }

        public override async Task<CategoryListResponse> GetAll(Empty request, ServerCallContext context)
        {
            List<ICategory> categories = await categoryRepository.GetAll().ToListAsync();

            return new CategoryListResponse { Categories = { categories.Select(grpcCategoryConverter.ConvertToGrpc) } };
        }

        public override async Task<CategoryListResponse> GetAllByParentCategoryId(
            ParentIdRequest request,
            ServerCallContext context
            )
        {
            List<ICategory> categories =
                await categoryRepository.GetAllByParentCategoryId(Guid.Parse(request.ParentId)).ToListAsync();

            return new CategoryListResponse { Categories = { categories.Select(grpcCategoryConverter.ConvertToGrpc) } };
        }

        public override async Task<GrpcCategory> Add(GrpcCategory grpcCategory, ServerCallContext context)
        {
            ICategory category = grpcCategoryConverter.ConvertToEntity(grpcCategory);
            category = await categoryRepository.AddAsync(category);

            return grpcCategoryConverter.ConvertToGrpc(category);
        }

        public override async Task<GrpcCategory> Update(GrpcCategory grpcCategory, ServerCallContext context)
        {
            ICategory category = grpcCategoryConverter.ConvertToEntity(grpcCategory);
            category = await categoryRepository.UpdateAsync(category);

            return grpcCategoryConverter.ConvertToGrpc(category);
        }

        public override async Task<Empty> DeleteById(CategoryIdRequest idRequest, ServerCallContext context)
        {
            ICategory category = await categoryRepository.GetByIdAsync(Guid.Parse(idRequest.Id));
            await categoryRepository.DeleteAsync(category);

            return new Empty();
        }
    }
}
