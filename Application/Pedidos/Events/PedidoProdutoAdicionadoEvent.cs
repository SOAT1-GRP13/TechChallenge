using Domain.Base.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedidos.Events
{
    public class PedidoProdutoAdicionadoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public PedidoProdutoAdicionadoEvent(Guid clienteId, Guid pedidoId, Guid produtoId, int quantidade)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
    }
}
