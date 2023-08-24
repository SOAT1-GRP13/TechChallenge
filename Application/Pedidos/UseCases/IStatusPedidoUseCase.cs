using Domain.Pedidos;
using Application.Pedidos.Queries.DTO;

namespace Application.Pedidos.UseCases
{
    public interface IStatusPedidoUseCase : IDisposable
    {
        Task<PedidoDto> TrocaStatusPedido(Guid idPedido, PedidoStatus novoStatus);
    }
}
