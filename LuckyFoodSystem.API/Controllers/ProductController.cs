using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Application.Menus.Queries.Read;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Application.Products.Queries.Read;
using LuckyFoodSystem.Contracts.Menu;
using LuckyFoodSystem.Contracts.Products;
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

            var query = new GetAllMenusQuery();

            ErrorOr<MenuResult> getingMenuResult = await _mediator.Send(query);

            return getingMenuResult.Match(
                getingMenuResult => Ok(_mapper.Map<ProductResponse>(getingMenuResult)),
                errors => Problem(errors));
        }

        [HttpGet("/product/{productId:guid}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid productId)
        {
            await Task.CompletedTask;

            var query = new GetProductByIdQuery(ProductId.Create(productId));

            ErrorOr<ProductResult> gettingByIdResult = await _mediator.Send(query);

            return gettingByIdResult.Match(
                gettingByIdResult => Ok(_mapper.Map<ProductResult>(gettingByIdResult)),
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
    }
}
