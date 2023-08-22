using Application.Pedidos.Queries.DTO;
using AutoMapper;
using Domain.Pedidos;

namespace Application.Pedidos.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoAppService(IPedidoRepository pedidoRepository,
                                IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<PedidoDto> ObterPorId(Guid id)
        {
            return _mapper.Map<PedidoDto>(await _pedidoRepository.ObterPorId(id));
        }

        public async Task<PedidoDto> AtualizarStatusPedido(Guid pedidoId, int status)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            pedido.AtualizarStatus((PedidoStatus)status);

            _pedidoRepository.Atualizar(pedido);

            await _pedidoRepository.UnitOfWork.Commit();

            return _mapper.Map<PedidoDto>(pedido);
        }

        public void Dispose()
        {
            _pedidoRepository?.Dispose();
        }
    }
}