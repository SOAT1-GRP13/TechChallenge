using Domain.Base.Messages;

namespace Application.Pedidos.Events
{
    public class PedidoProdutoRemovidoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }

        public PedidoProdutoRemovidoEvent(Guid clienteId, Guid pedidoId, Guid produtoId)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
        }
    }
}
