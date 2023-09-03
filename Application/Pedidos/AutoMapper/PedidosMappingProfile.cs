using AutoMapper;
using Domain.Pedidos;
using Application.Pedidos.Boundaries;
using Application.Pedidos.Queries.DTO;

namespace Application.Pedidos.AutoMapper
{
    public class PedidosMappingProfile : Profile
    {
        public PedidosMappingProfile()
        {
            CreateMap<Pedido, PedidoDto>();

            CreateMap<PedidoDto, PedidoOutput>();
        }
    }
}
