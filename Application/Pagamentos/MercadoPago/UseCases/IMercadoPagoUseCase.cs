using Domain.MercadoPago;

namespace Application.Pagamentos.MercadoPago.UseCases
{

    public interface IMercadoPagoUseCase
    {
        Task<MercadoPagoOrderStatus> PegaStatusPedido(long id);
    }
}
