using Application.Pedidos.Boundaries;
using Application.Pedidos.Queries.DTO;

namespace Application.Pedidos.Services
{
    public interface IPedidoAppService : IDisposable
    {
        Task<PedidoDto> ObterPorId(Guid id);
    }
}