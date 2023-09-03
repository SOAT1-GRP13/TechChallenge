using Domain.Pedidos;
using Application.Pedidos.Queries.DTO;
using Application.Pedidos.Boundaries;

namespace Application.Pedidos.UseCases
{
    public interface IPedidoUseCase : IDisposable
    {
        Task<bool> AdicionarItem(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario);

        Task<bool> AtualizarItem(Guid clienteId, Guid produtoId, int quantidade);

        Task<bool> RemoverItem(Guid clienteId, Guid produtoId);

        Task<PedidoDto> TrocaStatusPedido(Guid idPedido, PedidoStatus novoStatus);

        Task<ConfirmarPedidoOutput> IniciarPedido(Guid pedidoId);

        Task<bool> FinalizarPedido(Guid pedidoId);

        Task<bool> CancelarProcessamento(Guid pedidoId);

        Task<PedidoDto> ObterPedidoPorId(Guid pedidoId);
    }
}
