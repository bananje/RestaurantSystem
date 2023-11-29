using Grpc.Core;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using Mapster;
using MapsterMapper;

namespace LuckyFoodSystem.ProductApi.Grpc.Services
{
    public class ProductApiService : ProductService.ProductServiceBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductApiService(IProductRepository productRepository,
                                 IMapper mapper) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public override Task<ProductReply> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            var product 
            var product = _mapper.Adapt<Product>();
            return base.GetProductById(request, context);
        }
    }
}
