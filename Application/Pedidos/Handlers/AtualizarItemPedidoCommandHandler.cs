using MediatR;
using Application.Pedidos.UseCases;
using Application.Pedidos.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Base.DomainObjects;

namespace Application.Pedidos.Handlers
{
    public class AtualizarItemPedidoCommandHandler : IRequestHandler<AtualizarItemPedidoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoUseCase _pedidoUseCase;

        public AtualizarItemPedidoCommandHandler(
            IMediatorHandler mediatorHandler,
            IPedidoUseCase pedidoUseCase
        )
        {
            _mediatorHandler = mediatorHandler;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                foreach (var error in message.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

                return false;
            }

            bool retorno;
            try
            {
                retorno = await _pedidoUseCase.AtualizarItem(message.ClienteId, message.ProdutoId, message.Quantidade);
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
                return false;
            }

            if (!retorno)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, "Erro ao atualizar o item do pedido!"));
                return false;
            }

            return true;
        }
    }
}
