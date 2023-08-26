using Domain.Pedidos;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Pedidos.Boundaries
{
    public class PedidoOutput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Guid do pedido",
            Format = "Guid")]
        public Guid Id { get; set; }

        [SwaggerSchema(
            Title = "Codigo",
            Description = "Codigo do pedido",
            Format = "int")]
        public int Codigo { get; set; }

        [SwaggerSchema(
            Title = "ValorTotal",
            Description = "Valor total do pedido",
            Format = "decimal")]
        public decimal ValorTotal { get; set; }

        [SwaggerSchema(
            Title = "DataCadastro",
            Description = "Data de cadastro do pedido",
            Format = "DateTime")]
        public DateTime DataCadastro { get; set; }

        [SwaggerSchema(
            Title = "PedidoStatus",
            Description = "Status do pedido",
            Format = "PedidoStatus")]
        public PedidoStatus PedidoStatus { get; set; }

        [SwaggerSchema(
            Title = "MercadoPagoId",
            Description = "Id utilizado pelo webhook",
            Format = "int")]
        public int MercadoPagoId { get; set; }
    }
}
