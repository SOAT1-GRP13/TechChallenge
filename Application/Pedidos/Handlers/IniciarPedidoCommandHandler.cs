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
    public class IniciarPedidoCommandHandler : IRequestHandler<IniciarPedidoCommand, PedidoOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoUseCase _pedidoUseCase;
        private readonly IMapper _mapper;

        public IniciarPedidoCommandHandler(
            IMediatorHandler mediatorHandler,
            IPedidoUseCase pedidoUseCase
,
            IMapper mapper)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoUseCase = pedidoUseCase;
            _mapper = mapper;
        }

        public async Task<PedidoOutput> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                foreach (var error in message.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            try
            {
                await _pedidoUseCase.IniciarPedido(message.PedidoId);
                var pedidoDto = await _pedidoUseCase.ObterPedidoPorId(message.PedidoId);

                return _mapper.Map<PedidoOutput>(pedidoDto);
            }
            catch (Exception ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
            }

            return new PedidoOutput();
        }
    }
}
