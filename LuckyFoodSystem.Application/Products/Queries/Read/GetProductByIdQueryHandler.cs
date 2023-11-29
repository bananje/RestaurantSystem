using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public class GetProductByIdQueryHandler
        : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }            

        public async Task<ErrorOr<ProductResult>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var selectedProduct = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (selectedProduct is null)
            {
                return Errors.Global.ObjectNonExistentException;
            }

            return new ProductResult(new Product[] { selectedProduct }.ToList());
        }
    }
}
