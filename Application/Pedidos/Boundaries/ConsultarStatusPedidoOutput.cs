using Domain.Pedidos;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Pedidos.Boundaries
{
    public class ConsultarStatusPedidoOutput
    {
        public ConsultarStatusPedidoOutput()
        {
            Status = PedidoStatus.Rascunho;
        }

        public ConsultarStatusPedidoOutput(PedidoStatus status, Guid pedidoId)
        {
            Status = status;
            PedidoId = pedidoId;
        }

        [SwaggerSchema(
            Title = "Guid do pedido",
            Format = "Guid")]
        public Guid PedidoId { get; set; }

        [SwaggerSchema(
            Title = "Status",
            Format = "enum")]
        public PedidoStatus Status { get; set; }
    }
}