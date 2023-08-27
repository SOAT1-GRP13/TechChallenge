using Application.Pagamentos.MercadoPago.DTOs;
using Domain.Pedidos;

namespace Application;

public interface IMercadoPagoGateway
{
    Task<string> GeraPedidoQrCode(Pedido pedido);

    Task<MercadoPagoOrderStatus> PegaStatusPedido(long id);
}
