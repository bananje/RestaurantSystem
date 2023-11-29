using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public class GetProductsByMenuQueryHandler
        : IRequestHandler<GetProductsByMenuQuery, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsByMenuQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }                   
        public async Task<ErrorOr<ProductResult>> Handle(GetProductsByMenuQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            IEnumerable<Product>? selectedProducts =
                  await _productRepository.GetProductsByMenuAsync(request.MenuId, cancellationToken);

            if (selectedProducts is null || selectedProducts.Count() is 0)
            {
                return Errors.Global.CollectionNonExistentException;
            }

            return new ProductResult(selectedProducts.ToList());
        }
    }
}
