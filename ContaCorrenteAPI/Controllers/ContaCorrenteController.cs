using ContaCorrenteAPI.Domain.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        [HttpPost]
        [Route("registrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar(
            [FromServices] IMediator mediator,
            [FromBody] RegistrarContaCorrenteRequest command
        )
        {
            var response = await mediator.Send(command);

            if(response.Info.Successo)
            {
                return CreatedAtAction("RegistrarContaCorente", response);
            }

            return BadRequest(response.Info.MensagensDeErroValidacao);
        }


        [HttpPut]
        [Route("inativar")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Inativar(
            [FromServices] IMediator mediator,
            [FromBody] InativarContaCorrenteRequest command

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