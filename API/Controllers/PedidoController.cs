using Application.Pedidos.Boundaries;
using Application.Pedidos.Queries;
using Application.Pedidos.Queries.DTO;
using Application.Pedidos.Services;
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
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(IPedidoQueries pedidoQueries,
            IPedidoAppService pedidoAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _pedidoQueries = pedidoQueries;
            _pedidoAppService = pedidoAppService;
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

        [HttpPut("atualizar-status-pedido/{pedidoId}")]
        [SwaggerOperation(
            Summary = "Atualizar status do pedido",
            Description = "Atualiza o status do pedido")]
        [SwaggerResponse(200, "Retorna o pedido atualizado", typeof(PedidoDto))]
        [SwaggerResponse(404, "Caso não encontre o pedido com o Id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> AtualizarStatusPedido([FromRoute] Guid pedidoId, [FromBody] AtualizarStatusPedidoInput input)
        {
            try
            {
                var pedido = await _pedidoAppService.ObterPorId(pedidoId);
                if (pedido == null) return NotFound("Pedido não encontrado.");

                var pedidoAtualizado = await _pedidoAppService.AtualizarStatusPedido(pedidoId, input.Status);

                return Ok(pedidoAtualizado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar status do pedido. Erro: {ex.Message}");
            }           
        }
    }
}
