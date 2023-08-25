using Application.Pedidos.Commands;
using Application.Pedidos.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.DomainObjects.DTO;
using Domain.Base.Messages.CommonMessages.IntegrationEvents.Pedidos;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedidos.Handlers
{
    public class CancelarProcessamentoPedidoEstornarEstoqueCommandHandler : IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoUseCase _pedidoUseCase;

        public CancelarProcessamentoPedidoEstornarEstoqueCommandHandler(
            IMediatorHandler mediatorHandler,
            IPedidoUseCase pedidoUseCase
        )
        {
            _mediatorHandler = mediatorHandler;
            _pedidoUseCase = pedidoUseCase;
        }

        public async Task<bool> Handle(CancelarProcessamentoPedidoEstornarEstoqueCommand message, CancellationToken cancellationToken)
        {
            bool retorno;
            try
            {
                retorno = await _pedidoUseCase.CancelarProcessamentoEEstornarEstoque(message.PedidoId);
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
                return false;
            }

            if (!retorno)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, "Erro ao cancelar o processamento do pedido!"));
                return false;
            }

            return true;
        }
    }
}
