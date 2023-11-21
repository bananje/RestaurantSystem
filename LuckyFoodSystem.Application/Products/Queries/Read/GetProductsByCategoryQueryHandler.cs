using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public class GetProductsByCategoryQueryHandler
        : IRequestHandler<GetProductsByCategoryQuery, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }                   
        public async Task<ErrorOr<ProductResult>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            IEnumerable<Product>? selectedProducts = await _productRepository.GetProductsByCategoryAsync(request.categoryId);

            if (selectedProducts is null || selectedProducts.Count() is 0)
            {
                return Errors.Global.CollectionNonExistentException;
            }

            return new ProductResult(selectedProducts.ToList());
        }
    }
}
