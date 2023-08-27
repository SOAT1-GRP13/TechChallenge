using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Commands;
using Application.Pagamentos.Commands;
using Application.Pagamentos.MercadoPago.Boundaries;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("[Controller]")]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public MercadoPagoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [SwaggerOperation(
            Summary = "Webhook mercado Pago",
            Description = "Endpoint responsavel por receber um evento do mercado pago, no momento alterando o status do pedido para pago sempre")]
        [SwaggerResponse(200, "Retorna OK após alterar o status")]
        [SwaggerResponse(400, "Caso não seja preenchido todos os campos obrigatórios")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("Webhook")]
        public async Task<IActionResult> LogInUsuario([FromBody] WebHookInput input)
        {
            var command = new StatusPagamentoCommand(input);
            await _mediatorHandler.EnviarComando<StatusPagamentoCommand, bool>(command);

            if (OperacaoValida())
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
            }
        }


    }
}
