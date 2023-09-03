using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("[Controller]")]
    [SwaggerTag("Endpoints para relacionado ao gestão, é necessário se autenticar aqui caso queira utilizar o crud de produtos")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AutenticacaoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [SwaggerOperation(
            Summary = "Autenticação do Gestor/Atendente",
            Description = "Endpoint responsavel por autenticar o Gestor ou atendente")]
        [SwaggerResponse(200, "Retorna dados se autenticado ou não", typeof(LogInUsuarioOutput))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("LogInUsuario")]
        public async Task<IActionResult> LogInUsuario([FromBody] LogInUsuarioInput input)
        {
            var command = new AdminAutenticaCommand(input);
            var autenticado = await _mediatorHandler.EnviarComando<AdminAutenticaCommand, LogInUsuarioOutput> (command);

            if (OperacaoValida())
            {
                return Ok(autenticado);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
            }
        }


    }
}
