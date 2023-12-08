using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }       
        public async Task<ErrorOr<ProductResult>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productId = ProductId.Create(request.ProductId.Value);

            bool result = await _productRepository.RemoveProductAsync(productId, request.rootPath, cancellationToken);

            if (!result)
            {
                return Errors.Global.ObjectNotRemovedException;
            }

            return new ProductResult();
        }
    }
}
