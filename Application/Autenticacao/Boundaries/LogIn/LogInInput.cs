using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Autenticacao.Boundaries.LogIn
{
    public class LogInUsuarioInput
    {

        public LogInUsuarioInput() 
        { 
            NomeUsuario = string.Empty;
            Senha = string.Empty;
        }

        public LogInUsuarioInput(string nomeUsuario, string senha)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
        }


        [SwaggerSchema(
            Title = "Nome do Usuario",
            Description = "Preencha com um nome unico de acesso",
            Format = "string")]
       // [Required]
        public string NomeUsuario { get; set; }

        [SwaggerSchema(
            Title = "Senha",
            Description = "Preencha com a senha",
            Format = "string")]
       // [Required]
        public string Senha { get; set; }
    }
}
