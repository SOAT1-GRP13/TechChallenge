using Swashbuckle.AspNetCore.Annotations;

namespace Application.Pedidos.Boundaries
{
    public class IniciarPedidoInput
    {
        [SwaggerSchema(
            Title = "Id do pedido",
            Format = "Guid")]
        public Guid PedidoId { get; set; }

    }
}
