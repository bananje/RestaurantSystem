using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.API.Common;
using LuckyFoodSystem.Application.Products.Commands.Create;
using LuckyFoodSystem.Application.Products.Commands.Delete;
using LuckyFoodSystem.Application.Products.Commands.Update;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Application.Products.Queries.Read;
using LuckyFoodSystem.Contracts.Product;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuckyFoodSystem.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IMapper mapper,
                                 ISender mediator,
                                 IWebHostEnvironment webHostEnvironment)   
        {
            _mapper = mapper;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/products")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            await Task.CompletedTask;

            var query = new GetAllProductsQuery();

            ErrorOr<ProductResult> getingProductResult = await _mediator.Send(query);

            return getingProductResult.Match(
                getingProductResult => Ok(_mapper.Map<ProductResponse>(getingProductResult)),
                errors => Problem(errors));
        }

        [HttpGet("/product/{productId:guid}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid productId)
        {
            await Task.CompletedTask;

            var query = new GetProductByIdQuery(ProductId.Create(productId));

            ErrorOr<ProductResult> gettingByIdResult = await _mediator.Send(query);

            return gettingByIdResult.Match(
                gettingByIdResult => Ok(_mapper.Map<ProductResponse>(gettingByIdResult)),
                errors => Problem(errors));
        }

        [HttpGet("/products/{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategoryAsync(int categoryId)
        {
            await Task.CompletedTask;

            var query = new GetProductsByCategoryQuery(categoryId);

            ErrorOr<ProductResult> gettingByCategoryResult = await _mediator.Send(query);

            return gettingByCategoryResult.Match(
                gettingByCategoryResult => Ok(_mapper.Map<ProductResponse>(gettingByCategoryResult)),
                errors => Problem(errors));
        }

        [HttpGet("/products/{menuId:guid}")]
        public async Task<IActionResult> GetProductsByMenuAsync(Guid menuId)
        {
            await Task.CompletedTask;

            var query = new GetProductsByMenuQuery(MenuId.Create(menuId));

            ErrorOr<ProductResult> gettingByMenuResult = await _mediator.Send(query);

            return gettingByMenuResult.Match(
                gettingByMenuResult => Ok(_mapper.Map<ProductResponse>(gettingByMenuResult)),
                errors => Problem(errors));
        }

        [HttpPost("/product")]
        public async Task<IActionResult> СreateProductAsync([FromForm] CreateProductRequest request)
        {
            await Task.CompletedTask;

            string rootPath = _webHostEnvironment.WebRootPath + WC.ProductImagePath;
            var command = _mapper.Map<CreateProductCommand>((request, rootPath));

            ErrorOr<ProductResult> addingProductResult = await _mediator.Send(command);

            return addingProductResult.Match(
                addingProductResult => Ok(_mapper.Map<ProductResponse>(addingProductResult)),
                errors => Problem(errors));
        }

        [HttpDelete("/product/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveProductAsync(Guid productId)
        {
            await Task.CompletedTask;

            string rootPath = _webHostEnvironment.WebRootPath + WC.ProductImagePath;
            var command = new DeleteProductCommand(ProductId.Create(productId), rootPath);

            ErrorOr<ProductResult> deletingProductResult = await _mediator.Send(command);

            return deletingProductResult.Match(
                deletingProductResult => NoContent(),
                errors => Problem(errors));
        }

        [HttpPut("/product/{productId:guid}")]
        public async Task<IActionResult> UpdateProductAsync(Guid productId, [FromForm] UpdateProductRequest request)
        {
            await Task.CompletedTask;

            string rootPath = _webHostEnvironment.WebRootPath + WC.ProductImagePath;
            var command = _mapper.Map<UpdateProductCommand>((request, rootPath, productId));

            ErrorOr<ProductResult> updatingProductResult = await _mediator.Send(command);

            return updatingProductResult.Match(
                updatingMenuResult => Ok(_mapper.Map<ProductResponse>(updatingMenuResult)),
                errors => Problem(errors));
        }
    }
}
