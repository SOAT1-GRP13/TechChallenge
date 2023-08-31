using Application.Pedidos.Boundaries;
using Domain.Pedidos;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class ConsultarStatusPedidoOutputExample : IExamplesProvider<ConsultarStatusPedidoOutput>
    {
        public ConsultarStatusPedidoOutput GetExamples()
        {
            return new ConsultarStatusPedidoOutput
            {
                PedidoId = Guid.NewGuid(),
                Status = PedidoStatus.Iniciado,
                
            };
        }
    }
}
