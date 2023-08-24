using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Pedidos.Boundaries
{
    public class AtualizarStatusPedidoInput
    {
        [Required]
        [SwaggerSchema(
            Title = "Guid do pedido",
            Format = "Guid")]
        public Guid IdPedido { get; set; }

        [Required]
        [SwaggerSchema(
            Title = "Status",
            Format = "int")]
        public int Status { get; set; }
    }
}