using Application.Pedidos.Queries;
using Application.Pedidos.Queries.DTO;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("Pedidos")]
    [SwaggerTag("Endpoints relacionados a pedidos, não é necessário se autenticar")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IPedidoQueries pedidoQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _pedidoQueries = pedidoQueries;
        }

        [HttpGet("pedidos-por-cliente/{clientId}")]
        [SwaggerOperation(
            Summary = "Listar pedidos por cliente",
            Description = "Lista os pedidos pelo id do cliente")]
        [SwaggerResponse(200, "Retorna pedidos do cliente", typeof(IEnumerable<PedidoDto>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> PedidosPorCliente([FromRoute] Guid clientId)
        {
            return Ok(await _pedidoQueries.ObterPedidosCliente(clientId));
        }

        [HttpGet("pedidos")]
        [SwaggerOperation(
            Summary = "Lista todos os pedidos",
            Description = "Lista todos pedidos")]
        [SwaggerResponse(200, "Retorna pedidos idependente do status", typeof(IEnumerable<PedidoDto>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Pedidos()
        {
            return Ok(await _pedidoQueries.ObterTodosPedidos());
        }
    }
}
