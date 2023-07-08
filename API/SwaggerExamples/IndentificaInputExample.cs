using Application.Autenticacao.Boundaries.Cliente;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class IndentificaInputExample : IExamplesProvider<IdentificaInput>
    {
        public IdentificaInput GetExamples()
        {
            return new IdentificaInput("01438749007", "Teste@123");
        }
    }
}
