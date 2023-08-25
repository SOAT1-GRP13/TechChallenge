using Domain.Pedidos;
using Application.Pedidos.Queries.DTO;

namespace Application.Pedidos.UseCases
{
    public interface IPedidoUseCase : IDisposable
    {
        Task<bool> AdicionarItem(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario);

        Task<bool> AtualizarItem(Guid clienteId, Guid produtoId, int quantidade);

        Task<bool> RemoverItem(Guid clienteId, Guid produtoId);

        Task<PedidoDto> TrocaStatusPedido(Guid idPedido, PedidoStatus novoStatus);

        Task<bool> IniciarPedido(Guid clienteId, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao);

        Task<bool> FinalizarPedido(Guid pedidoId);

        Task<bool> CancelarProcessamento(Guid pedidoId);

        Task<bool> CancelarProcessamentoEEstornarEstoque(Guid pedidoId);
    }
}
