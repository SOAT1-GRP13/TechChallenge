using Domain.Pedidos;

namespace Domain.MercadoPago
{
    public interface IMercadoPagoRepository
    {
        Task<string> GeraPedidoQrCode(Pedido pedido);

        Task<MercadoPagoOrderStatus> PegaStatusPedido(long id);
    }
}
