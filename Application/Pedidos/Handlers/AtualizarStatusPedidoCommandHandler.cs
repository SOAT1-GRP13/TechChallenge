using MediatR;
using Domain.Pedidos;
using Application.Pedidos.Commands;
using Application.Pedidos.UseCases;
using Application.Pedidos.Boundaries;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using AutoMapper;
using Domain.Base.DomainObjects;
using Domain.Base.Messages;

namespace Application.Pedidos.Handlers
{
    public class AtualizarStatusPedidoCommandHandler : IRequestHandler<AtualizarStatusPedidoCommand, PedidoOutput>
    {
        private readonly IPedidoUseCase _pedidoUseCase;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public AtualizarStatusPedidoCommandHandler(
            IPedidoUseCase statusPedidoUseCase,
            IMediatorHandler mediatorHandler,
            IMapper mapper
        )
        {
            _pedidoUseCase = statusPedidoUseCase;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<PedidoOutput> Handle(AtualizarStatusPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedidoVazio = new PedidoOutput();

            if (!request.EhValido())
            {
                foreach (var error in request.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, error.ErrorMessage));
                return pedidoVazio;
            }

            try
            {
                var input = request.Input;
                var pedidoDto = await _pedidoUseCase.TrocaStatusPedido(input.IdPedido, (PedidoStatus)input.Status);

                if (pedidoDto.Codigo == 0)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Pedido não encontrado"));
                    return pedidoVazio;
                }

                return _mapper.Map<PedidoOutput>(pedidoDto);
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, ex.Message));
                return pedidoVazio;
            }
        }
    }
}
