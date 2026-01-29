using ContaCorrenteAPI.Domain.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        [HttpPost]
        [Route("registrar")]
        [Authorize]
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
