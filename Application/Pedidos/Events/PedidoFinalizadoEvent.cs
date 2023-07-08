using Domain.Base.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedidos.Events
{
    public class PedidoFinalizadoEvent : Event
    {
        public Guid PedidoId { get; private set; }

        public PedidoFinalizadoEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
            AggregateId = pedidoId;
        }
    }
}
