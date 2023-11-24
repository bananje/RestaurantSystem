using LuckyFoodSystem.Application.Common.Interfaces.Persistence;

namespace LuckyFoodSystem.ProductApi.Grpc.Services
{
    public class ProductApiService : ProductService.ProductServiceBase
    {
        private readonly IProductRepository _productRepository;
        public ProductApiService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

    }
}
