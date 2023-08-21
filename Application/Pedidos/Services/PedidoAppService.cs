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
    }
}