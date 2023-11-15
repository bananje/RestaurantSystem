using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.API.Common;
using LuckyFoodSystem.Application.Menus.Commands.Create;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Application.Menus.Queries.Read;
using LuckyFoodSystem.Contracts.Menu;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuckyFoodSystem.API.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(IMapper mapper,
                              ISender mediator,
                              IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("/menu")]
        public async Task<IActionResult> AddMenuAsync([FromForm] CreateMenuRequest request)
        {
            string rootPath = _webHostEnvironment.WebRootPath;
            var command = _mapper.Map<CreateMenuCommand>((request, rootPath + WC.MenuImagePath));

            ErrorOr<MenuResult> addindMenuResult = await _mediator.Send(command);                       

            return addindMenuResult.Match(
                addindMenuResult => Ok(_mapper.Map<MenuResponse>(addindMenuResult)),
                errors => Problem(errors));
        }

        [HttpGet("/menus")]
        public async Task<IActionResult> GetAllMenusAsync()
        {
            var query = new GetAllMenusQuery();

            ErrorOr<MenuResult> getingMenuResult = await _mediator.Send(query);
            
            return getingMenuResult.Match(
                getingMenuResult => Ok(_mapper.Map<MenuResponse>(getingMenuResult)),
                errors => Problem(errors));
        }

        [HttpGet("/menu/{menuId:guid}")]
        public async Task<IActionResult> GetMenuByIdAsync(Guid menuId)
        {
            var query = new GetMenuByIdQuery(MenuId.Create(menuId));

            ErrorOr<MenuResult> gettingByIdResult = await _mediator.Send(query);

            return gettingByIdResult.Match(
                gettingByIdResult => Ok(_mapper.Map<MenuResponse>(gettingByIdResult)),
                errors => Problem(errors));
        }

        [HttpGet("/menu/{categoryId:int}")]
        public async Task<IActionResult> GetMenuByIdAsync(int categoryId)
        {
            var query = new GetMenuByCategoryQuery(categoryId);

            ErrorOr<MenuResult> gettingByCategoryResult = await _mediator.Send(query);

            return gettingByCategoryResult.Match(
                gettingByCategoryResult => Ok(_mapper.Map<MenuResponse>(gettingByCategoryResult)),
                errors => Problem(errors));
        }

    }
}
