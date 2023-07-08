using System;
using Domain.Base.Messages;

namespace Application.Pedidos.Commands
{
    public class FinalizarPedidoCommand : Command
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }

        public FinalizarPedidoCommand(Guid pedidoId, Guid clienteId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }
    }
}