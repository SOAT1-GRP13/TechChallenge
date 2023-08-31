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
    public class ConsultarStatusPedidoCommandHandler : IRequestHandler<ConsultarStatusPedidoCommand, ConsultarStatusPedidoOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoUseCase _pedidoUseCase;

        public ConsultarStatusPedidoCommandHandler(
            IMediatorHandler mediatorHandler,
            IPedidoUseCase pedidoUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<ConsultarStatusPedidoOutput> Handle(ConsultarStatusPedidoCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                try
                {
                    var pedido = await _pedidoUseCase.ObterPedidoPorId(request.Id);

                    return new ConsultarStatusPedidoOutput(pedido.PedidoStatus, request.Id);
                }
                catch (DomainException ex)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, ex.Message));
                }
            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }
            return new ConsultarStatusPedidoOutput();
        }
    }
}
