using Application.Pedidos.Queries.DTO;


namespace Application.Pedidos.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId);
        Task<IEnumerable<PedidoDto>> ObterTodosPedidos();

    }
}
