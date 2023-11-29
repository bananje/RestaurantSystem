using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate.Enumerations;
using LuckyFoodSystem.AggregationModels.Common.Enumerations;

namespace LuckyFoodSystem.Application.Products.Commands.Create
{
    public class CreateProductCommandHandler
        : IRequestHandler<CreateProductCommand, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;   
        }
        public async Task<ErrorOr<ProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(new Title(request.Title),
                                         new Price(request.Price),
                                         new Description(request.Description),
                                         new ShortDescription(request.ShortDescription),
                                         new Weight(request.WeightValue, WeightUnits.FromName(request.WeightUnit)),
                                         Category.FromName(request.Category));

            await _productRepository.AddProductAsync(product, request.MenusIds, request.rootPath, cancellationToken);

            return new ProductResult(new Product[] { product }.ToList());
        }
    }
}
