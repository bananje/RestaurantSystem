using ErrorOr;
using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.Enumerations;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using LuckyFoodSystem.Domain.AggregationModels.ProductAggregate.ValueObjects;
using MediatR;


namespace LuckyFoodSystem.Application.Products.Commands.Update
{
    internal class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ErrorOr<ProductResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {    
            var productId = ProductId.Create(request.ProductId);
            var updatedProduct = Product.Set(productId,
                                          new Title(request.Title),
                                          new Price(request.Price),
                                          new Description(request.Description),
                                          new ShortDescription(request.ShortDescription),
                                          new Weight(request.WeightValue, WeightUnits.FromName(request.WeightUnit)),
                                          Category.FromName(request.Category));

            Product? existedProduct = await _productRepository.GetProductByIdAsync(productId);
            if(existedProduct is null)
            {
                return Errors.Global.ObjectNonExistentException;
            }         
            
            Product newProduct = await _productRepository.UpdateProductAsync(productId, updatedProduct, request.rootPath,
                                                        cancellationToken, request.ImagesIds, request.AddingMenusIds, request.DeletingMenusIds);

            return new ProductResult(new Product[] { newProduct }.ToList());
        }    
    }
}
