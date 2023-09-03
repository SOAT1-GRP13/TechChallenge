using Application.Pedidos.Queries.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class CarrinhoDtoExample : IExamplesProvider<CarrinhoDto>
    {
        public CarrinhoDto GetExamples()
        {
            return new CarrinhoDto
            {
                PedidoId = new Guid("6a8f3649-0987-49c7-8997-5d68aec97c42"),
                ClienteId = new Guid("4885e451-b0e4-4490-b959-04fabc806d32"),
                SubTotal = 10,
                ValorTotal = 10,
                Items = new List<CarrinhoItemDto>()
                {
                    new CarrinhoItemDto()
                    {
                        ProdutoId = new Guid("903562cf-1368-4e93-9de3-93f88b1407be"),
                        ProdutoNome = "Coca-Cola - Lata",
                        Quantidade = 2,
                        ValorUnitario = 5,
                        ValorTotal = 10
                    }
                }
            };
        }
    }
}
