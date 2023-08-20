using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Autenticacao.Boundaries.Cliente
{
    public class AutenticaClienteInput
    {
        public AutenticaClienteInput(string cPF, string senha)
        {
            CPF = cPF;
            Senha = senha;
        }

        [SwaggerSchema(
            Title = "CPF",
            Description = "Preencha com um CPF válido",
            Format = "string")]
        public string CPF { get; set; }

        [SwaggerSchema(
            Title = "Senha",
            Description = "Preencha com a senha",
            Format = "string")]
        public string Senha { get; set; }
    }
}
