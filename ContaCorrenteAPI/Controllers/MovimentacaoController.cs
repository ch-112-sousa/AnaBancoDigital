using ContaCorrenteAPI.Domain.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteAPI.Controllers
{
    public class MovimentacaoController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        //[Authorize]
        public async Task<IActionResult> Create(
               [FromServices] IMediator mediator,
               [FromBody] RegistrarMovimentacaoRequest command
           )
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
