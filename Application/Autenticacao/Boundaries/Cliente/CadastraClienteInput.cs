using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Autenticacao.Boundaries.Cliente
{
    public class CadastraClienteInput
    {
        public CadastraClienteInput(string cPF, string senha, string email, string nome)
        {
            CPF = cPF;
            Senha = senha;
            Email = email;
            Nome = nome;
        }


        [SwaggerSchema(
            Title = "CPF",
            Description = "Preencha com um CPF válido",
            Format = "string")]
        [Required]
        public string CPF { get; set; }

        [SwaggerSchema(
            Title = "Senha",
            Description = "Preencha com a senha",
            Format = "string")]
        [Required]
        public string Senha { get; set; }


        [SwaggerSchema(
            Title = "E-mail",
            Description = "Preencha com um E-mail válido",
            Format = "string")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [SwaggerSchema(
            Title = "Nome",
            Description = "Preencha com um nome",
            Format = "string")]
        [Required]
        public string Nome { get; set; }
    }
}
