using Swashbuckle.AspNetCore.Annotations;

namespace Application.Pedidos.Boundaries
{

    public class ConfirmarPedidoOutput
    {
        public ConfirmarPedidoOutput()
        {
            QrData = string.Empty;
            PedidoId = Guid.NewGuid();
        }

        public ConfirmarPedidoOutput(string qrData, Guid pedidoId)
        {
            QrData = qrData;
            PedidoId = pedidoId;
        }


        [SwaggerSchema(
            Title = "QrData",
            Description = "String para gerar QrCode do mercado Pago",
            Format = "string")]
        public string QrData { get; set; }

        [SwaggerSchema(
            Title = "Id do pedido",
            Description = "Id do pedido para atualizar o status manualmente",
            Format = "Guid")]
        public Guid PedidoId { get; set; }
    }
}
