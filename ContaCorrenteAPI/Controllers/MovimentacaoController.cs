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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar(
               [FromServices] IMediator mediator,
               [FromBody] RegistrarMovimentacaoRequest command
           )
        {
            var response = await mediator.Send(command);

            if (response.Info.Successo)
            {
                return NoContent();
            }

            return BadRequest(response.Info.MensagensDeErroValidacao);
        }
    }
}