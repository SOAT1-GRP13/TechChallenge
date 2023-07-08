using Application.Autenticacao.Boundaries.Cliente;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class CadastraClienteInputExample : IExamplesProvider<CadastraClienteInput>
    {
        public CadastraClienteInput GetExamples()
        {
            return new CadastraClienteInput("82720528064", "123456", "teste@provedor.com", "José Silva");
        }
    }
}
