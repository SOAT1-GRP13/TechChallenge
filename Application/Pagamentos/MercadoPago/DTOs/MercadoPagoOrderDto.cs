using System.Globalization;
using Domain.Pedidos;

namespace Application.Pagamentos.MercadoPago.DTOs
{
    public class MercadoPagoOrderDto
    {
        public MercadoPagoOrderDto()
        {
            External_reference = string.Empty;
            Title = string.Empty;
            Notification_url = string.Empty;
            Description = string.Empty;
            Expiration_date = DateTime.Now.ToString("o", CultureInfo.InvariantCulture);
            Total_amount = 0;
            Items = new List<OrderItemDto>();
        }

        public MercadoPagoOrderDto(Pedido pedido, List<OrderItemDto> orderItems)
        {
            External_reference = pedido.Id.ToString();
            Title = "Pedido confirmado"; //TODO preencher com titulo do pedido
            Notification_url = "https://webhook.site/5a39a921-2433-4068-9e81-a39ee64f5133"; //TODO preencher com URL do webhook
            Description = "Descrição do pedido";
            Expiration_date = DateTime.Now.AddMinutes(20).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffzzz");
            Total_amount = pedido.ValorTotal;
            Items = orderItems;
        }

        public string External_reference { get; set; }
        public string Title { get; set; }
        public string Notification_url { get; set; }
        public string Description { get; set; }
        public string Expiration_date { get; set; }
        public decimal Total_amount { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}