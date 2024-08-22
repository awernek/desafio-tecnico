using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentoController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MovimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo movimento na conta corrente.
        /// </summary>
        /// <param name="command">Os dados do movimento a ser criado.</param>
        /// <returns>Retorna o resultado da operação de criação do movimento.</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CriarMovimentoRequest command)
        {
            var response = await _mediator.Send(command);
            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Obtém o saldo atual de uma conta corrente.
        /// </summary>
        /// <param name="query">Os parâmetros de consulta para obter o saldo.</param>
        /// <returns>Retorna o saldo atual da conta corrente.</returns>
        [HttpGet]
        [Route("saldo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSaldo([FromQuery] ObterMovimentoRequest query)
        {
            var response = await _mediator.Send(query);
            if (!response.Sucesso)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
