using Application.Pedidos.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.IntegrationEvents.Pedidos;
using MediatR;

namespace Application.Pedidos.Events
{
    public class PedidoEventHandler :
            INotificationHandler<PedidoRascunhoIniciadoEvent>,
            INotificationHandler<PedidoItemAdicionadoEvent>,
            INotificationHandler<PedidoEstoqueRejeitadoEvent>,
            INotificationHandler<PedidoPagamentoRealizadoEvent>,
            INotificationHandler<PedidoPagamentoRecusadoEvent>
    {

        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PedidoEstoqueRejeitadoEvent message, CancellationToken cancellationToken)
        {
            //cancelar o processamento do pedido e retornar para rascunho ou outra lógica que o negócio decidir
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoPagamentoRealizadoEvent message, CancellationToken cancellationToken)
        {
            // Após o pagamento realizado, virá futuramente um evento que o pedido deve ser enviado para cozinha
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoPagamentoRecusadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.PedidoId, message.ClienteId));
        }
    }
}

