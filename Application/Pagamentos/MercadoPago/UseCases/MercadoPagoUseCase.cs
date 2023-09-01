
using Application.Pagamentos.MercadoPago.Boundaries;
using Application.Pagamentos.MercadoPago.UseCases;
using Domain.MercadoPago;
using Domain.Pedidos;

namespace Application.Pagamentos.MercadoPago.Gateways
{
    public class MercadoPagoUseCase : IMercadoPagoUseCase
    {
        private readonly IMercadoPagoRepository _mercadoPagoRepository;

        public MercadoPagoUseCase(IMercadoPagoRepository mercadoPagoRepository)
        {
            _mercadoPagoRepository = mercadoPagoRepository;
        }

        public async Task<MercadoPagoOrderStatus> PegaStatusPedido(long id)
        {
            return await _mercadoPagoRepository.PegaStatusPedido(id);
        }
    }

}