using Application.Pedidos.Boundaries;
using Application.Pedidos.Commands;
using Application.Pedidos.UseCases;
using AutoMapper;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;

namespace Application.Pedidos.Handlers
{
    public class IniciarPedidoCommandHandler : IRequestHandler<IniciarPedidoCommand, ConfirmarPedidoOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoUseCase _pedidoUseCase;

        public IniciarPedidoCommandHandler(
            IMediatorHandler mediatorHandler,
            IPedidoUseCase pedidoUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<ConfirmarPedidoOutput> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                foreach (var error in message.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            try
            {
                var qrData = await _pedidoUseCase.IniciarPedido(message.PedidoId);

                return qrData;
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
            }

            return new ConfirmarPedidoOutput();
        }
    }
}
