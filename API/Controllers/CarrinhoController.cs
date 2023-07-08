using Application.Catalogo.Services;
using Application.Pedidos.Boundaries;
using Application.Pedidos.Commands;
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
    [Route("Carrinho")]
    [SwaggerTag("Endpoints relacionados ao carrinho, não é necessário se autenticar")]
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public CarrinhoController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService,
                                  IMediatorHandler mediatorHandler,
                                  IPedidoQueries pedidoQueries) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatorHandler = mediatorHandler;
            _pedidoQueries = pedidoQueries;
        }


        [HttpPost("adicionar-item")]
        [SwaggerOperation(
            Summary = "Adicionar Item ao carrinho",
            Description = "Adiciona o item desejado ao carrinho")]
        [SwaggerResponse(200, "Retorna dados do carrinho", typeof(CarrinhoDto))]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio", typeof(IEnumerable<string>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> AdicionarItem([FromBody] AdicionarItemInput input)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorId(input.Id);
                if (produto == null) return NotFound();


                var command = new AdicionarItemPedidoCommand(ObterClienteId(), produto.Id, produto.Nome, input.Quantidade, produto.Valor);
                await _mediatorHandler.EnviarComando(command);

                if (OperacaoValida()) // Verifica se tem notificacoes de erro
                {
                    //Se estiver tudo certo, retorna o carrinho atualizado
                    return Ok(await _pedidoQueries.ObterCarrinhoCliente(ObterClienteId()));

                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
                }

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                                          $"Erro ao tentar adicionar item ao carrinho. Erro: {ex.Message}");
            }

        }

        [HttpPut("atualizar-item")]
        [SwaggerOperation(
            Summary = "Atualizar item do carrinho",
            Description = "Atualiza o item desejado no carrinho")]
        [SwaggerResponse(200, "Retorna dados do carrinho", typeof(CarrinhoDto))]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio", typeof(IEnumerable<string>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> AtualizarItem([FromBody] AtualizarItemInput input)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorId(input.Id);
                if (produto == null) return NotFound();

                var command = new AtualizarItemPedidoCommand(ObterClienteId(), input.Id, input.Quantidade);
                await _mediatorHandler.EnviarComando(command);

                if (OperacaoValida())
                {
                    return Ok(await _pedidoQueries.ObterCarrinhoCliente(ObterClienteId()));
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                                         $"Erro ao tentar atualizar item do carrinho. Erro: {ex.Message}");
            }
        }

        [HttpDelete("remover-item/{id}")]
        [SwaggerOperation(
            Summary = "Remover item do carrinho",
            Description = "Remove o item desejado no carrinho")]
        [SwaggerResponse(200, "Retorna dados do carrinho", typeof(CarrinhoDto))]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio", typeof(IEnumerable<string>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> RemoverItem([FromRoute] Guid id)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorId(id);
                if (produto == null) return NotFound();

                var command = new RemoverItemPedidoCommand(ObterClienteId(), id);
                await _mediatorHandler.EnviarComando(command);

                if (OperacaoValida())
                {
                    return Ok(await _pedidoQueries.ObterCarrinhoCliente(ObterClienteId()));
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                                          $"Erro ao tentar remover item do carrinho. Erro: {ex.Message}");
            }



        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar itens do carrinho",
            Description = "Lista os itens no carrinho")]
        [SwaggerResponse(200, "Retorna dados do carrinho", typeof(CarrinhoDto))]
        [SwaggerResponse(404, "Caso não encontre nenhum carrinho")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [Route("meu-carrinho")]
        public async Task<IActionResult> MeuCarrinho()
        {
            try
            {
                var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ObterClienteId());
                if (carrinho is null)
                    return this.StatusCode(StatusCodes.Status404NotFound, "Nenhum carrinho em rascunho encontrado");

                return Ok(await _pedidoQueries.ObterCarrinhoCliente(ObterClienteId()));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                                          $"Erro ao tentar recuperar carrinho. Erro: {ex.Message}");
            }
        }


        [HttpPost("confirmar-pedido-pagamento")]
        [SwaggerOperation(
            Summary = "Atualizar item do carrinho",
            Description = "Atualiza o item desejado no carrinho")]
        [SwaggerResponse(200, "Retorna pedidos do cliente", typeof(IEnumerable<PedidoDto>))]
        [SwaggerResponse(404, "Caso não encontre nenhum carrinho")]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> RealizarPagamentoPedido([FromBody] CarrinhoDto carrinhoDto)
        {
            try
            {
                var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ObterClienteId());

                if (carrinho is null)
                    return this.StatusCode(StatusCodes.Status404NotFound, "Nenhum carrinho em rascunho encontrado");

                //IniciarPedidoCommand Dispara todos os eventos de dominio para criar o pedido, realizar pagamento e finalizar pedido.
                var command = new IniciarPedidoCommand(carrinho.PedidoId, ObterClienteId(), carrinho.ValorTotal, carrinhoDto.Pagamento.NomeCartao,
                    carrinhoDto.Pagamento.NumeroCartao, carrinhoDto.Pagamento.ExpiracaoCartao, carrinhoDto.Pagamento.CvvCartao);

                await _mediatorHandler.EnviarComando(command);

                if (OperacaoValida())
                {
                    return Ok(await _pedidoQueries.ObterPedidosCliente(ObterClienteId()));
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                                          $"Erro ao tentar confirmar pedido. Erro: {ex.Message}");
            }
        }
    }
}
