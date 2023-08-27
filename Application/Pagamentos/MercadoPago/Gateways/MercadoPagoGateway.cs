using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Pagamentos.MercadoPago.DTOs;
using Domain.Pedidos;

namespace Application.Pagamentos.MercadoPago.Gateways
{
    public class MercadoPagoGateway : IMercadoPagoGateway
    {
        public async Task<string> GeraPedidoQrCode(Pedido pedido)
        {
            var itensList = new List<OrderItemDto>();

            foreach (var orderItem in pedido.PedidoItems.ToList())
            {
                itensList.Add(new OrderItemDto(orderItem));
            }

            var dto = new MercadoPagoOrderDto(pedido, itensList);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.mercadopago.com/instore/orders/qr/seller/collectors/185446979/pos/FFFC01/qrs");
            request.Headers.Add("Authorization", "Bearer TEST-2316387072181620-082610-d8fcda27dadc2f3c607e557511117fd6-185446979");

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var order = JsonSerializer.Serialize(dto, serializeOptions);
            var content = new StringContent(order, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var qrResponse = JsonSerializer.Deserialize<MercadoPagoQrResponse>(await response.Content.ReadAsStringAsync(), serializeOptions);
                return qrResponse!.Qr_data;
            }
            return string.Empty;
        }

        public async Task<MercadoPagoOrderStatus> PegaStatusPedido(long id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.mercadopago.com/merchant_orders/" + id.ToString());
            request.Headers.Add("Authorization", "Bearer TEST-2316387072181620-082610-d8fcda27dadc2f3c607e557511117fd6-185446979");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

                var statusResponse = JsonSerializer.Deserialize<MercadoPagoOrderStatus>(await response.Content.ReadAsStringAsync(), serializeOptions);
                return statusResponse!;
            }

            return new MercadoPagoOrderStatus();
        }
    }

}