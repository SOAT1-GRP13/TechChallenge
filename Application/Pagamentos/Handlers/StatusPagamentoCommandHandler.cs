using Application.Pagamentos.Commands;
using Application.Pedidos.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Pedidos;
using MediatR;

namespace Application.Pagamentos.Handlers
{
    public class StatusPagamentoCommandHandler :
        IRequestHandler<StatusPagamentoCommand, bool>
    {

        private readonly IPedidoUseCase _pedidoUseCase;
        private readonly IMediatorHandler _mediatorHandler;

        public StatusPagamentoCommandHandler(IPedidoUseCase pedidoUseCase, IMediatorHandler mediatorHandler)
        {
            _pedidoUseCase = pedidoUseCase;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(StatusPagamentoCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                var input = request.Input;

                //TODO pegar o Id do input e chamar o endpoint do mercado pago para ver se o pagamento foi pago ou não
                await _pedidoUseCase.TrocaStatusPedidoWebhook(input.Id, PedidoStatus.Pago);

                return true;
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