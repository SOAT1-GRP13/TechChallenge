using MediatR;
using Domain.Base.DomainObjects;
using Application.Pedidos.Commands;
using Application.Pedidos.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;

namespace Application.Pedidos.Handlers
{
    public class FinalizarPedidoCommandHandler : IRequestHandler<FinalizarPedidoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoUseCase _pedidoUseCase;

        public FinalizarPedidoCommandHandler(
            IMediatorHandler mediatorHandler,
            IPedidoUseCase pedidoUseCase
        )
        {
            _mediatorHandler = mediatorHandler;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<bool> Handle(FinalizarPedidoCommand message, CancellationToken cancellationToken)
        {
            bool retorno;
            try
            {
                retorno = await _pedidoUseCase.FinalizarPedido(message.PedidoId);
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
                return false;
            }

            if (!retorno)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, "Erro ao finalizar o pedido!"));
                return false;
            }

            return true;
        }
    }
}
