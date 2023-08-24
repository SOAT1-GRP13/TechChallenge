using Application.Pedidos.Boundaries;
using Application.Pedidos.Queries.DTO;
using AutoMapper;
using Domain.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedidos.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoUseCase(
            IPedidoRepository pedidoRepository,
            IMapper mapper
        )
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<PedidoDto> TrocaStatusPedido(Guid idPedido, PedidoStatus novoStatus)
        {
            var pedido = await _pedidoRepository.ObterPorId(idPedido);

            if (pedido is null)
                return new PedidoDto();

            pedido.AtualizarStatus(novoStatus);

            _pedidoRepository.Atualizar(pedido);

            await _pedidoRepository.UnitOfWork.Commit();

            return _mapper.Map<PedidoDto>(pedido);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

        }
    }
}
