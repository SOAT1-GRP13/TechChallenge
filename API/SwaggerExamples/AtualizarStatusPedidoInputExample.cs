using Application.Pedidos.Boundaries;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class AtualizarStatusPedidoInputExample : IExamplesProvider<AtualizarStatusPedidoInput>
    {
        public AtualizarStatusPedidoInput GetExamples()
        {
            return new AtualizarStatusPedidoInput
            {
                IdPedido = new Guid("bdf2f9b7-087d-4f06-a34f-1278c694c609"),
                Status = 1
            };
        }
    }
}
