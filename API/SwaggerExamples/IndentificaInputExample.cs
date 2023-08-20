using Application.Autenticacao.Boundaries.Cliente;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class IndentificaInputExample : IExamplesProvider<AutenticaClienteInput>
    {
        public AutenticaClienteInput GetExamples()
        {
            return new AutenticaClienteInput("01438749007", "Teste@123");
        }
    }
}
