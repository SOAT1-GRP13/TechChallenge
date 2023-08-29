using Domain.Base.DomainObjects.DTO;
using Domain.Pedidos;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Pedidos.Boundaries
{
    public class PedidoNaFilaOutput
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
            Title = "Itens",
            Description = "Itens do pedido",
            Format = "List<Item>")]
        public List<Item> Itens { get; set; }

        public class Item
        {
            [SwaggerSchema(
                Title = "ProdutoId",
                Description = "Guid do produto",
                Format = "Guid")]
            public Guid ProdutoId { get; set; }

            [SwaggerSchema(
                Title = "ProdutoNome",
                Description = "Nome do produto",
                Format = "string")]
            public string ProdutoNome { get; set; }

            [SwaggerSchema(
                Title = "Quantidade",
                Description = "Quantidade do produto",
                Format = "int")]
            public int Quantidade { get; set; }
        }
    }


}
