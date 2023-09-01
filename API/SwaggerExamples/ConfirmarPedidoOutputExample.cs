using Application.Pedidos.Boundaries;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class ConfirmarPedidoOutputExample : IExamplesProvider<ConfirmarPedidoOutput>
    {
        public ConfirmarPedidoOutput GetExamples()
        {
            return new ConfirmarPedidoOutput
            {
                PedidoId = Guid.NewGuid(),
                QrData = "string qr data"
            };
        }
    }
}
