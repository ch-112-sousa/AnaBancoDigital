using ContaCorrenteAPI.Domain.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContaCorrenteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        //[Authorize]
        public async Task<IActionResult> Create(
            [FromServices] IMediator mediator,
            [FromBody] CreateContaCorrenteRequest command
        )
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }


        [HttpPut("inativar")]
        [Authorize]
        public async Task<ActionResult> Inativar(
            [FromServices] IMediator mediator,
            [FromBody] InativarContaCorrenteRequest command

            )
        {
            var response = await mediator.Send(command);

            if(string.IsNullOrWhiteSpace(response.Error))
            {
                return NoContent();
            }

             return BadRequest(response.Error);            
        }
    }
}