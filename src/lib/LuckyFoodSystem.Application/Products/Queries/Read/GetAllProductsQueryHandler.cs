using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public class GetAllProductsQueryHandler
        : IRequestHandler<GetAllProductsQuery, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ErrorOr<ProductResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await _productRepository.GetProductsAsync(cancellationToken);
            if (products.Count() is 0)
            {
                return Errors.Global.CollectionNonExistentException;
            }

            return new ProductResult(products.ToList());
        }       
    }
}
