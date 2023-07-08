using Domain.Base.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pedidos
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);

        Task<PedidoItem> ObterItemPorId(Guid id);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);

        Task<IEnumerable<Pedido>> ObterTodosPedidos();

    }
}
