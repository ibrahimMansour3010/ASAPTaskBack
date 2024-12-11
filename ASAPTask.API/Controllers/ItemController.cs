using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Item.Commands.CreateItem;
using ASAPTask.Applications.Item.Commands.CreateItem.Dtos;
using ASAPTask.Applications.Item.Commands.DeleteItem;
using ASAPTask.Applications.Item.Commands.DeleteItem.Dtos;
using ASAPTask.Applications.Item.Commands.UpdateItem;
using ASAPTask.Applications.Item.Commands.UpdateItem.Dtos;
using ASAPTask.Applications.Item.Queries.ItemList;
using ASAPTask.Applications.Item.Queries.ItemList.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASAPTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : BaseController
    {
        [HttpGet("GetItemList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<ItemListOutput>))]
        public async Task<IActionResult> GetItemList([FromQuery] ItemListQuery query)
            => Ok(await Mediator.Send(query));


        [HttpPost("CreateItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<CreateItemOutput>))]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command)
            => Ok(await Mediator.Send(command));


        [HttpPut("UpdateItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<UpdateItemOutput>))]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command)
            => Ok(await Mediator.Send(command));

        [HttpDelete("DeleteItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<DeleteItemOutput>))]
        public async Task<IActionResult> DeleteItem([FromBody] DeleteItemCommand command)
            => Ok(await Mediator.Send(command));
    }
}
