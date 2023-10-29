using System.Text;
using System.Text.Json;
using Domain.Configuration;
using Domain.MercadoPago;
using Domain.Pedidos;
using Microsoft.Extensions.Options;

namespace Infra.MercadoPago.Repository
{
    public class MercadoPagoRepository : IMercadoPagoRepository
    {
        private readonly Secrets _settings;

        public MercadoPagoRepository(IOptions<Secrets> options)
        {
            _settings = options.Value;
        }

        public async Task<string> GeraPedidoQrCode(Pedido pedido)
        {
            var itensList = new List<OrderItem>();

            foreach (var orderItem in pedido.PedidoItems.ToList())
            {
                itensList.Add(new OrderItem(orderItem));
            }

            var dto = new MercadoPagoOrder(pedido, itensList, _settings.Notification_url);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.mercadopago.com/instore/orders/qr/seller/collectors/{_settings.MercadoPagoUserId}/pos/{_settings.External_Pos_Id}/qrs");
            request.Headers.Add("Authorization", $"Bearer {_settings.AccesToken}");

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
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.mercadopago.com/merchant_orders/{id}");
            request.Headers.Add("Authorization", $"Bearer {_settings.AccesToken}");
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