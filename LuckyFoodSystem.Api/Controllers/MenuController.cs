using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Constants;
using LuckyFoodSystem.Application.Menus.Commands.Create;
using LuckyFoodSystem.Application.Menus.Commands.Delete;
using LuckyFoodSystem.Application.Menus.Commands.Update;
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
        
        [HttpGet("/menus")]
        public async Task<IActionResult> GetAllMenusAsync()
        {
            await Task.CompletedTask;

            var query = new GetAllMenusQuery();

            ErrorOr<MenuResult> getingMenuResult = await _mediator.Send(query);
            
            return getingMenuResult.Match(
                getingMenuResult => Ok(_mapper.Map<MenuResponse>(getingMenuResult)),
                errors => Problem(errors));
        }

        [HttpGet("/menu/{menuId:guid}")]
        public async Task<IActionResult> GetMenuByIdAsync(Guid menuId)
        {
            await Task.CompletedTask;

            var query = new GetMenuByIdQuery(MenuId.Create(menuId));

            ErrorOr<MenuResult> gettingByIdResult = await _mediator.Send(query);

            return gettingByIdResult.Match(
                gettingByIdResult => Ok(_mapper.Map<MenuResponse>(gettingByIdResult)),
                errors => Problem(errors));
        }

        [HttpGet("/menu/{categoryId:int}")]
        public async Task<IActionResult> GetMenuByCategoryAsync(int categoryId)
        {
            await Task.CompletedTask;

            var query = new GetMenuByCategoryQuery(categoryId);

            ErrorOr<MenuResult> gettingByCategoryResult = await _mediator.Send(query);

            return gettingByCategoryResult.Match(
                gettingByCategoryResult => Ok(_mapper.Map<MenuResponse>(gettingByCategoryResult)),
                errors => Problem(errors));
        }

        [HttpPost("/menu")]
        public async Task<IActionResult> CreateMenuAsync([FromForm] CreateMenuRequest request)
        {
            await Task.CompletedTask;

            string rootPath = _webHostEnvironment.WebRootPath + PathConstants.MenuImagePath;
            var command = _mapper.Map<CreateMenuCommand>((request, rootPath));

            ErrorOr<MenuResult> addingMenuResult = await _mediator.Send(command);

            return addingMenuResult.Match(
                addindMenuResult => Ok(_mapper.Map<MenuResponse>(addindMenuResult)),
                errors => Problem(errors));
        }

        [HttpDelete("/menu/{menuId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMenuAsync(Guid menuId)
        {
            await Task.CompletedTask;

            string rootPath = _webHostEnvironment.WebRootPath + PathConstants.MenuImagePath;
            var command = new DeleteMenuCommand(MenuId.Create(menuId), rootPath);

            ErrorOr<MenuResult> deletingMenuResult = await _mediator.Send(command);

            return deletingMenuResult.Match(
                addindMenuResult => NoContent(),
                errors => Problem(errors));
        }

        [HttpPut("/menu/{menuId:guid}")]
        public async Task<IActionResult> UpdateMenu(Guid menuId, [FromForm] UpdateMenuRequest request)
        {
            await Task.CompletedTask;

            string rootPath = _webHostEnvironment.WebRootPath + PathConstants.MenuImagePath;
            var command = _mapper.Map<UpdateMenuCommand>((request, rootPath, menuId));

            ErrorOr<MenuResult> updatingMenuResult = await _mediator.Send(command);

            return updatingMenuResult.Match(
                updatingMenuResult => Ok(_mapper.Map<MenuResponse>(updatingMenuResult)),
                errors => Problem(errors));
        }
    }
}
