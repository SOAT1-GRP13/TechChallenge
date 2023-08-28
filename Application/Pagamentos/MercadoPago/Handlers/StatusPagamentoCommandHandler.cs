using Application.Pagamentos.MercadoPago.Commands;
using Application.Pedidos.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Pedidos;
using MediatR;

namespace Application.Pagamentos.MercadoPago.Handlers
{
    public class StatusPagamentoCommandHandler :
        IRequestHandler<StatusPagamentoCommand, bool>
    {

        private readonly IPedidoUseCase _pedidoUseCase;
        private readonly IMercadoPagoGateway _mercadoPagoGateway;
        private readonly IMediatorHandler _mediatorHandler;

        public StatusPagamentoCommandHandler(IPedidoUseCase pedidoUseCase,
         IMediatorHandler mediatorHandler, IMercadoPagoGateway mercadoPagoGateway)
        {
            _pedidoUseCase = pedidoUseCase;
            _mediatorHandler = mediatorHandler;
            _mercadoPagoGateway = mercadoPagoGateway;
        }

        public async Task<bool> Handle(StatusPagamentoCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                try
                {
                    var pedidoStatus = await _mercadoPagoGateway.PegaStatusPedido(request.Id);

                    if (pedidoStatus.Status == "closed")
                    {
                        await _pedidoUseCase.TrocaStatusPedido(Guid.Parse(pedidoStatus.External_reference), PedidoStatus.Pago);
                    }
                    else if (pedidoStatus.Status == "expired")
                    {
                        await _pedidoUseCase.TrocaStatusPedido(Guid.Parse(pedidoStatus.External_reference), PedidoStatus.Cancelado);
                    }

                    return true;
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
            return false;
        }
    }
}