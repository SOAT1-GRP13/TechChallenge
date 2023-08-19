using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [SwaggerTag("Endpoints relacionados ao cliente, não é necessário se autenticar")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClienteController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [SwaggerOperation(
            Summary = "Identificação do cliente",
            Description = "Endpoint responsavel por autenticar o cliente")]
        [SwaggerResponse(200, "Retorna dados se autenticado ou não", typeof(AutenticaClienteOutput))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("AutenticaCliente")]
        public async Task<IActionResult> AutenticaCliente([FromBody] AutenticaClienteInput input)
        {
            var command = new AutenticaClienteCommand(input);
            var autenticado = await _mediatorHandler.EnviarComando<AutenticaClienteCommand, AutenticaClienteOutput>(command);

            if (OperacaoValida())
            {
                return Ok(autenticado);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
            }
        }

        [SwaggerOperation(
    Summary = "Cadastro do cliente",
    Description = "Endpoint responsavel por cadastrar o cliente")]
        [SwaggerResponse(200, "Cadastra o usuario e ja o autentica", typeof(AutenticaClienteOutput))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("CadastraCliente")]
        public async Task<IActionResult> CadastraCliente([FromBody] CadastraClienteInput input)
        {
            var command = new CadastraClienteCommand(input);
            var autenticado = await _mediatorHandler.EnviarComando<CadastraClienteCommand, AutenticaClienteOutput>(command);

            if (OperacaoValida())
            {
                return Ok(autenticado);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
            }
        }


    }
}
