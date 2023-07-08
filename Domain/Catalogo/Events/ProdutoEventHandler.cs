using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.IntegrationEvents.Pedidos;
using MediatR;

namespace Domain.Catalogo.Events
{
    public class ProdutoEventHandler :
            INotificationHandler<ProdutoAbaixoEstoqueEvent>,
            INotificationHandler<PedidoIniciadoEvent>,
            INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProdutoEventHandler(IProdutoRepository produtoRepository,
                                   IEstoqueService estoqueService,
                                   IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);

            // Enviar um notificacao/email para aquisicao de mais produtos.
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
        {
            //TODO - Descomentar linha abaixo para utilizar controle de estoque nos pedidos
            //var result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);
            var result = true;

            if (result)
            {
                //Dispara evento que será capturado pelo serviço de pagamento em PagamentoEventHandler
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(message.PedidoId, message.ClienteId, message.Total, message.ProdutosPedido, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(message.PedidoId, message.ClienteId));
            }
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent message, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporListaProdutosPedido(message.ProdutosPedido);
        }
    }
}
