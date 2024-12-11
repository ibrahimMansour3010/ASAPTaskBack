using ASAPTask.Applications.Account.Commands.Login;
using ASAPTask.Applications.Account.Commands.Login.Dtos;
using ASAPTask.Applications.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASAPTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(contentType: "application/json", Type = typeof(IResult<LoginOutput>))]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
