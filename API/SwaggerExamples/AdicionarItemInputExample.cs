using Application.Pedidos.Boundaries;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class AdicionarItemInputExample : IExamplesProvider<AdicionarItemInput>
    {
        public AdicionarItemInput GetExamples()
        {
            return new AdicionarItemInput
            {
                Id = new Guid("903562cf-1368-4e93-9de3-93f88b1407be"),
                Quantidade = 2
            };
        }
    }
}
