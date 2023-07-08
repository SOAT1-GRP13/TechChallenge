using Application.Autenticacao.Boundaries.Cliente;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class IdentificaOutputExamples : IMultipleExamplesProvider<IdentificaOutput>
    {
        public IEnumerable<SwaggerExample<IdentificaOutput>> GetExamples()
        {
            yield return SwaggerExample.Create("Autenticado com sucesso", new IdentificaOutput("01438749007", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"));

            yield return SwaggerExample.Create("Falha de Autenticação", new IdentificaOutput(false, "Usuário e senha inválidos"));
        }
    }
}
