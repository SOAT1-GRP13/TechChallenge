using Application.Pedidos.Boundaries;
using Application.Pedidos.Queries.DTO;


namespace Application.Pedidos.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId);
        Task<IEnumerable<PedidoDto>> ObterTodosPedidos();
        Task<IEnumerable<PedidoNaFilaOutput>> ObterPedidosParaFila();

    }
}
