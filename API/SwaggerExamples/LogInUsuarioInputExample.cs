using Application.Autenticacao.Boundaries.LogIn;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class LogInUsuarioInputExample : IExamplesProvider<LogInUsuarioInput>
    {
        public LogInUsuarioInput GetExamples()
        {
            return new LogInUsuarioInput("fiapUser", "Teste@123");
        }
    }
}
